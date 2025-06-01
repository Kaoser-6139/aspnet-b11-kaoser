using Inventory.Domain;
using Inventory.Domain.Dtos;
using Inventory.Domain.Entities;
using Inventory.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public ProductService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public void AddProduct(Product product)
        {
           _applicationUnitOfWork.ProductRepository.Add(product);
            _applicationUnitOfWork.Save();
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetProducts(int pageIndex, int pageSize, string? order, ProductSearchDto searchDto)
        {
            return await _applicationUnitOfWork.GetProducts(pageIndex, pageSize, order, searchDto);
        }
    }
}
