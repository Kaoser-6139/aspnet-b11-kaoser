using AutoMapper;
//using Demo.Application.Exceptions;
//using Demo.Domain;
//using Demo.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using DevSkill.Inventory.Web.Domain;
using DevSkill.Inventory.Web.Infrastructure;
using Inventory.Application.Exceptions;
using Inventory.Domain.Entities;
using Inventory.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class CustomersController(
        ILogger<CustomersController> logger,
        ICustomerService customerService,
        IMapper mapper
            ) : Controller
    {
        private readonly ILogger<CustomersController> _logger = logger;
        private readonly ICustomerService _customerService = customerService;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Add()
        {
            var model = new AddCustomerModel();
            return View(model);
        }
        [HttpPost ,ValidateAntiForgeryToken,]
        public IActionResult Add(AddCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<Customer>(model);
                    customer.Id = IdentityGenerator.NewSequentialGuid();
                    _customerService.AddCustomer(customer);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                       Message="Author added",
                       Type=ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateCustomerNameException de)
                {
                    ModelState.AddModelError("DuplicateAuthor", de.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger
                    });
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
            var model = new UpdateCustomerModel();
            var customer=_customerService.GetCustomer(id);
            _mapper.Map(customer, model);
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken, Authorize(Policy = "UserAddPermissionHR")]
        public IActionResult Update(UpdateCustomerModel model)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var customer = _mapper.Map<Customer>(model);
                    _customerService.Update(customer);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Customer updated",
                        Type = ResponseTypes.Success

                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateCustomerNameException de)
                {
                    ModelState.AddModelError("DuplicateCustomer", de.Message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = de.Message,
                        Type = ResponseTypes.Danger

                    });

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update Customer");
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Failed to update Customer",
                        Type = ResponseTypes.Danger

                    });

                }
            }
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken, Authorize(Policy = "UserAddPermission")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Customer deleted",
                    Type = ResponseTypes.Success

                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to delete customer");
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Failed to delete customer",
                    Type = ResponseTypes.Danger

                });


            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult GetCustomerJsonData([FromBody] CustomerListModel model)
        {
            try
            {
                var (data, total, totalDisplay) = _customerService.GetCustomers(model.PageIndex,
                    model.PageSize, model.FormatSortExpression("SerialNumber","Name", "Mobile", "Address", "Email",
                    "CurrentBalance", "Status","Id"), model.Search);

                var customers = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data=(from record in data
                          select new string[]
                          {
                               HttpUtility.HtmlEncode(record.SerialNumber),
                              HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Mobile),
                                HttpUtility.HtmlEncode(record.Address),
                                  HttpUtility.HtmlEncode(record.Email),
                                   record.CurrentBalance.ToString(),
                                HttpUtility.HtmlEncode(record.Status),
                               record.Id.ToString()
                          }).ToArray()

                };
              
                return Json(customers);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "There was a problem in getting customers.");
                return Json(DataTables.EmptyResult);
            }
        }
    }
}
