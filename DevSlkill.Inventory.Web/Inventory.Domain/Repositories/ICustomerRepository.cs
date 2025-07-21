using Demo.Domain;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        (IList<Customer> data, int total, int totalDisplay) GetPagedCustomers(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        bool IsNameDuplicate(string name, Guid? id = null);
    }
}
