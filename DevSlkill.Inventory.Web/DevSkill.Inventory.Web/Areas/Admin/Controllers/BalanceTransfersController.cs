using AutoMapper;
using DevSkill.Inventory.Web.Domain;
using DevSkill.Inventory.Web.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Inventory.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BalanceTransfersController(IBalanceTransferService balance,
        ILogger<BalanceTransfersController> logger,
       IMapper mapper) : Controller
    {
        private readonly IBalanceTransferService _balanceTransferService = balance;
        private readonly ILogger<BalanceTransfersController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Add()
        {
            var model = new AddBalanceTransferModel();
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Add(AddBalanceTransferModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var balanceTransfer = _mapper.Map<BalanceTransfer>(model);
                    balanceTransfer.Id = IdentityGenerator.NewSequentialGuid();
                    _balanceTransferService.AddBalanceTransfer(balanceTransfer);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Balance Transfer Succeed",
                        Type = ResponseTypes.Success
                    });
                   return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add author");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to add Author",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            var model=new UpdateBalanceTransferModel();
            var balanceTransfer=_balanceTransferService.GetBalanceTransferReport(id);
            _mapper.Map(balanceTransfer,model);

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(UpdateBalanceTransferModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   var balanceTransfer= _mapper.Map<BalanceTransfer>(model);
                    _balanceTransferService.Update(balanceTransfer);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message="Updated Successfully",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");


                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to update");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message="Failed to Update",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _balanceTransferService.Delete(id);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Deleted Successfully",
                        Type = ResponseTypes.Success

                    });
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Failed to delete ");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to delete ",
                        Type = ResponseTypes.Danger

                    });


                }
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public JsonResult GetBalanceTransferJsonData([FromBody]BalanceTransferListModel model)
        {
            try
            {
                var (data, total, totalDisplay) = _balanceTransferService.GetBalanceTransfer(model.PageIndex,
                    model.PageSize, model.FormatSortExpression("SerialNumber", "Date",
                    "FromAccount", "ToAccount", "TransferAmount", "Note", "Id"), model.Search);

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
                                HttpUtility.HtmlEncode(record.FromAccount),
                                  HttpUtility.HtmlEncode(record.ToAccount),
                                   record.TransferAmount.ToString(),
                                HttpUtility.HtmlEncode(record.Note),
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
