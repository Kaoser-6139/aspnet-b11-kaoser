using AutoMapper;
using DevSkill.Inventory.Web.Application.Exceptions;
using DevSkill.Inventory.Web.Domain;
using DevSkill.Inventory.Web.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
//using DevSkill.Inventory.Web.Areas.Admin.Models;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Inventory.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;
        public SalesController(ILogger<SalesController> logger, IMapper mapper, ISaleService saleService)
        {
            _logger = logger;
            _mapper = mapper;
            _saleService = saleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
           var model=new AddSaleModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(AddSaleModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var sale = _mapper.Map<Sale>(model);
                    sale.Id = IdentityGenerator.NewSequentialGuid();
                    _saleService.AddCustomer(sale);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = " added successfully",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateCustomerNameException de)
                {
                    ModelState.AddModelError("Duplicate", de.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add sales report");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to add sale",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            var model=new UpdateSaleModel();
            var sale=_saleService.GetSale(id);
            _mapper.Map(sale, model);
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Update(UpdateSaleModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var sale=_mapper.Map<Sale>(model);
                    _saleService.Update(sale);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "report updated",
                        Type = ResponseTypes.Success

                    });
                    return RedirectToAction("Index");

                }
                catch (DuplicateCustomerNameException de)
                {
                    ModelState.AddModelError("Duplicate", de.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger

                    });

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update ");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to update report",
                        Type = ResponseTypes.Danger

                    });

                }

            }

            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _saleService.DeleteSale(id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sale deleted",
                    Type = ResponseTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete sale");
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message="Failed to delete",
                    Type = ResponseTypes.Danger
                });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult GetSalesJsonData([FromBody] SalestListModel model )
        {
            try
            {
                var (data, total, totalDisplay) = _saleService.GetSales(model.PageIndex,
                    model.PageSize, model.FormatSortExpression("SerialNumber", "Date",
                    "CustomerName","Total","Paid","Due", "Status", "Id"), model.Search);

                var customers = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                               HttpUtility.HtmlEncode(record.SerialNumber),
                             // HttpUtility.HtmlEncode(record.InvoiceNumber),
                                HttpUtility.HtmlEncode(record.Date),
                                HttpUtility.HtmlEncode(record.CustomerName),
                                 record.Total.ToString(),
                                  record.Paid.ToString(),
                                   record.Due.ToString(),
                                HttpUtility.HtmlEncode(record.Status),
                               record.Id.ToString()
                            }).ToArray()

                };

                return Json(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem in getting sales information.");
                return Json(DataTables.EmptyResult);
            }
        }
    }
}
