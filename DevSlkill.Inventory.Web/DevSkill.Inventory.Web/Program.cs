using Autofac.Extensions.DependencyInjection;
using Autofac;
using DevSkill.Inventory.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevSkill.Inventory.Web;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Inventory.Infrastructure;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Inventory.Application.Features.Products.Commands;
using Inventory.Infrastructure.Extensions;

var configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();



try 
{

    Log.Information("Application Starting..");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly=Assembly.GetExecutingAssembly();
    #region Autofac configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule(connectionString,migrationAssembly.FullName));
    });
    #endregion

    #region Automapper Configuration
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Serilog Configuration
    builder.Host.UseSerilog((context, lc) => lc
   .MinimumLevel.Debug()
   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)                                //https://chatgpt.com/share/6771889f-2e90-800c-b57b-fa8c554326d0
   .Enrich.FromLogContext()
   .ReadFrom.Configuration(builder.Configuration)
   );
    #endregion

    #region MediatR Configuration
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(migrationAssembly);
        cfg.RegisterServicesFromAssembly(typeof(ProductAddCommand).Assembly);
    });
    #endregion

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString,(x)=>x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    #region Identity Configuration
    builder.Services.AddIdentity();
    #endregion

    #region Authorization Configuration
    builder.Services.AddPolicy();
    #endregion
    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

  

    app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
     .WithStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapRazorPages()
       .WithStaticAssets();

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Application crashed");

}
finally
{
   Log.CloseAndFlush();
}
