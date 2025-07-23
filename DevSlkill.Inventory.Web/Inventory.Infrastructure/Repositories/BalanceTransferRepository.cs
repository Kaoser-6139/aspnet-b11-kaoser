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
    public  class BalanceTransferRepository:Repository<BalanceTransfer,Guid>,IBalanceTransferRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BalanceTransferRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public (IList<BalanceTransfer>, int total, int totalDisplay) GetPagedBalanceTransfer(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return GetDynamic(x => x.Note.Contains(search.Value) , order,
                   null, pageIndex, pageSize, true);
        }
    }
}
