using Demo.Domain;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Inventory.Application.Features.Products.Commands;
using Inventory.Application.Features.Products.Queries;
using Inventory.Domain.Entities;
using Inventory.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator,
            ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var model=new ProductAddCommand(); 
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductAddCommand productAddCommand)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(productAddCommand);
            }
            return View(productAddCommand);

        }
        public async Task<IActionResult> Update(Guid id)
        {
            var product = await _mediator.Send(new GetUpdatedProductById
            {
                Id = id
            });
                
            if(product==null)
            {
                return NotFound();
            }
            var model = new ProductUpdateCommand
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult>Update(ProductUpdateCommand productUpdateCommand)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(productUpdateCommand);
                return RedirectToAction("Index");
            }

            return View(productUpdateCommand);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new ProductDeleteCommand { Id = id });
            return RedirectToAction("Index");
            
        }


        [HttpPost]
        public async Task<JsonResult> GetProductsJsonDataAsync([FromBody] GetProductsQuery productsQuery)
        {
            try
            {
              var (data,total,totalDisplay)=await _mediator.Send(productsQuery);
                var products = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Description),
                                record.Price.ToString(),
                                record.Id.ToString()
                            }).ToArray()
                };
                return Json(products);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "There was a problem in getting products");
                return Json(DataTables.EmptyResult);
            }


        }
    }
}
