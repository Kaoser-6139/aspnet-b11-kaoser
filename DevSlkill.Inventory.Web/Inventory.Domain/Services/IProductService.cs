using Inventory.Domain.Dtos;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Services
{
    public interface IProductService
    {
        public void AddProduct(Product product);
        Task<(IList<Product> data,int total,int totalDisplay)> GetProducts(int pageIndex, int pageSize,
            string? order, ProductSearchDto searchDto);
    }
}
