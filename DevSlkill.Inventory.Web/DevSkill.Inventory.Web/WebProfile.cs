using AutoMapper;
using Inventory.Application.Features.Products.Commands;
using Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Web
{
    public class WebProfile:Profile
    {
        public WebProfile() 
        {
            CreateMap<ProductSearchModel,ProductSearchDto>().ReverseMap();
        }
    }
}
