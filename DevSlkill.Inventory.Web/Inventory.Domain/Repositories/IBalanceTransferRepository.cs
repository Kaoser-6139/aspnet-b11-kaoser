using Demo.Domain;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface IBalanceTransferRepository : IRepository<BalanceTransfer, Guid>
    {
        (IList<BalanceTransfer>, int total, int totalDisplay) GetPagedBalanceTransfer(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
