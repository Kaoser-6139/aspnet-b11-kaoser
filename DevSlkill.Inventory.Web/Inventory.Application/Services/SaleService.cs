using DevSkill.Inventory.Web.Domain;
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
    public  class SaleService:ISaleService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public SaleService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void AddCustomer(Sale sale)
        {
          _applicationUnitOfWork.SalesRepository.Add(sale);
            _applicationUnitOfWork.Save();
        }

        public void DeleteSale(Guid id)
        {
            _applicationUnitOfWork.SalesRepository.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public object GetSale(Guid id)
        {
          return  _applicationUnitOfWork.SalesRepository.GetById(id);
        }

        public (IList<Sale>, object total, object totalDisplay) GetSales(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
           return _applicationUnitOfWork.SalesRepository.GetPagedSales(pageIndex, pageSize, order, search);
        }

        public void Update(Sale sale)
        {
           _applicationUnitOfWork.SalesRepository.Update(sale);
            _applicationUnitOfWork.Save();

        }
    }
}
