using Inventory.Domain.Entities;
using Inventory.Domain.Features.Products.Queries;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public  class ProductRepository:Repository<Product,Guid>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext context) 
            :base(context)
        {
            _dbContext = context;
        }

        public async Task<(IList<Product>, int, int)> GetPagedProductsAsync(IGetProductsQuery request)
        {
            return await GetDynamicAsync(
               x=>x.Name.Contains(request.Search.Value)||
              x.Description != null && x.Description.Contains(request.Search.Value),
               request.FormatSortExpression("Name","Description","Price"),
               null,
               request.PageIndex,
               request.PageSize,
               true

                );
        }
    }
}
