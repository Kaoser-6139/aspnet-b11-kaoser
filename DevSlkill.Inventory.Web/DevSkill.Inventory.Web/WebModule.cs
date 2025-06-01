using Autofac;
using DevSkill.Inventory.Web.Data;
using Inventory.Application.Features.Products.Commands;
using Inventory.Application.Services;
using Inventory.Domain;
using Inventory.Domain.Repositories;
using Inventory.Domain.Services;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Web
{
    public class WebModule:Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductAddCommand>().AsSelf();
            base.Load(builder);
        }
    }
}
