using DevSkill.Inventory.Web.Domain;
using DevSkill.Inventory.Web.Infrastructure;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
    {
        public readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public (IList<Customer> data, int total, int totalDisplay) GetPagedCustomers(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {

            if (string.IsNullOrWhiteSpace(search.Value))
                return GetDynamic(null, order, null, pageIndex, pageSize, true);
            else
                return GetDynamic(x => x.Name.Contains(search.Value) ||
                x.Mobile.Contains(search.Value) || x.Email.Contains(search.Value), order,
                    null, pageIndex, pageSize, true);
        }

        public bool IsNameDuplicate(string name,Guid? id=null)
        {
            if (id.HasValue)
            {
                return GetCount(x=>x.Id!=id.Value && x.Name==name) > 0;
            }
            else
                return GetCount(x=>x.Name==name)>0;
        }

      
    }
}
