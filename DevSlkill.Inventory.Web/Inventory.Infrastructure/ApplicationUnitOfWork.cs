using Demo.Infrastructure.Utilities;
using DevSkill.Inventory.Web.Infrastructure;
using Inventory.Domain;
using Inventory.Domain.Dtos;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public  class ApplicationUnitOfWork:UnitOfWork,IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext context,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            ISaleRepository saleRepository,
            IBalanceTransferRepository balanceTransferRepository) : base(context)
        {
            ProductRepository = productRepository;
            CustomerRepository = customerRepository;
            SalesRepository=saleRepository;
            BalanceTransferRepository = balanceTransferRepository;


        }

        public IProductRepository ProductRepository { get; private set; }

        public ICustomerRepository CustomerRepository{get;private set;}

        public ISaleRepository SalesRepository {  get; private set; }

        public IBalanceTransferRepository BalanceTransferRepository { get; private set; }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetProducts(int pageIndex,
            int pageSize, string? order, ProductSearchDto search)
        {
            var procedureName = "GetProducts";

            var result = await SqlUtility.QueryWithStoredProcedureAsync<Product>(procedureName,
                new Dictionary<string, object>
                {
                    { "PageIndex", pageIndex },
                    { "PageSize", pageSize },
                    { "OrderBy", order },
                    { "PriceFrom", search.PriceFrom },
                    { "PriceTo", search.PriceTo },
                    { "Name", string.IsNullOrEmpty(search.Name) ? null : search.Name },
                    { "Description", string.IsNullOrEmpty(search.Description) ? null : search.Description }
                },
                new Dictionary<string, Type>
                {
                    { "Total", typeof(int) },
                    { "TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
    }
}
