using Demo.Domain;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Services
{
    public interface IBalanceTransferService
    {
        void AddBalanceTransfer(BalanceTransfer balanceTransfer);
        void Delete(Guid id);
        (IList<BalanceTransfer>, int total, int totalDisplay) GetBalanceTransfer(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        BalanceTransfer GetBalanceTransferReport(Guid id);
        void Update(BalanceTransfer balanceTransfer);
    }
}
