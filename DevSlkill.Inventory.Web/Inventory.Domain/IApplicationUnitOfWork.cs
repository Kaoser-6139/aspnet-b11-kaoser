using Inventory.Domain;
using Inventory.Domain.Dtos;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public  interface IApplicationUnitOfWork:IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public ISaleRepository SalesRepository { get; }

        Task<(IList<Product> data, int total, int totalDisplay)> GetProducts(int pageIndex, int pageSize, string? order, ProductSearchDto searchDto);
    }
}
