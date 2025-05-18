using Inventory.Domain.Entities;
using Inventory.Domain.Features.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<(IList<Product>, int, int)> GetPagedProductsAsync(IGetProductsQuery request);
       // Task UpdateAsync(Product product);
    }
}
