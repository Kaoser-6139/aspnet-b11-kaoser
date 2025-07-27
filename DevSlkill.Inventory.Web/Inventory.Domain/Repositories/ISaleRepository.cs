using DevSkill.Inventory.Web.Domain;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface ISaleRepository : IRepository<Sale, Guid>
    {
        (IList<Sale>, object total, object totalDisplay) GetPagedSales(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
