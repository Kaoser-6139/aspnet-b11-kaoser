using Demo.Domain;
using Inventory.Domain;
using Inventory.Domain.Entities;
using Inventory.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public  class BalanceTransferService:IBalanceTransferService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public BalanceTransferService(IApplicationUnitOfWork applicationUnitOfWork) 
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void AddBalanceTransfer(BalanceTransfer balanceTransfer)
        {
           _applicationUnitOfWork.BalanceTransferRepository.Add(balanceTransfer);
            _applicationUnitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            _applicationUnitOfWork.BalanceTransferRepository.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public (IList<BalanceTransfer>, int total, int totalDisplay) GetBalanceTransfer(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return _applicationUnitOfWork.BalanceTransferRepository.GetPagedBalanceTransfer(pageIndex, pageSize, order, search);
        }

        public BalanceTransfer GetBalanceTransferReport(Guid id)
        {
            return _applicationUnitOfWork.BalanceTransferRepository.GetById(id);
        }

        public void Update(BalanceTransfer balanceTransfer)
        {
            _applicationUnitOfWork.BalanceTransferRepository.Update(balanceTransfer);
            _applicationUnitOfWork.Save();
        }
    }
}
