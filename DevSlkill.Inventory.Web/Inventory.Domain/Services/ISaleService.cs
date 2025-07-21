using Demo.Domain;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Services
{
    public interface ISaleService
    {
        void AddCustomer(Sale sale);
        void DeleteSale(Guid id);
        object GetSale(Guid id);
        (IList<Sale>, object total, object totalDisplay) GetSales(int pageIndex, int pageSize, 
            string? order, DataTablesSearch search);
        void Update(Sale sale);
    }
}
