using Demo.Domain;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class SaleRepository:Repository<Sale,Guid>,ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context) 
            : base(context)
        { 

        }

        public (IList<Sale>, object total, object totalDisplay) GetPagedSales(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return GetDynamic(x => 
               x.CustomerName.Contains(search.Value) , order,
                   null, pageIndex, pageSize, true);
        }
    }
}
