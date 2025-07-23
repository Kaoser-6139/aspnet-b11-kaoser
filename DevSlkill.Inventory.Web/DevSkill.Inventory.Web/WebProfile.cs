using AutoMapper;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Inventory.Application.Features.Products.Commands;
using Inventory.Domain.Dtos;
using Inventory.Domain.Entities;

namespace DevSkill.Inventory.Web
{
    public class WebProfile:Profile
    {
        public WebProfile() 
        {
            CreateMap<ProductSearchModel,ProductSearchDto>().ReverseMap();
            CreateMap<AddCustomerModel,Customer>().ReverseMap();
            CreateMap<UpdateCustomerModel,Customer>().ReverseMap();
            CreateMap<AddSaleModel,Sale>().ReverseMap();
            CreateMap<UpdateSaleModel,Sale>().ReverseMap();
            CreateMap<AddBalanceTransferModel,BalanceTransfer>().ReverseMap();
            CreateMap<UpdateBalanceTransferModel,BalanceTransfer>().ReverseMap();
        }
    }
}
