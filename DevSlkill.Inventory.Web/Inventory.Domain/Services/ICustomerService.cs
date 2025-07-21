using Demo.Domain;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Services
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(Guid id);
        Customer  GetCustomer(Guid id);
        (IList<Customer> data, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize, 
            string? order, DataTablesSearch search);
        void Update(Customer customer);
    }
}
