using DevSkill.Inventory.Web.Application.Exceptions;
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
    public  class CustomerService:ICustomerService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public CustomerService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public void AddCustomer(Customer customer)
        {
            if (!_applicationUnitOfWork.CustomerRepository.IsNameDuplicate(customer.Name))
            {
                _applicationUnitOfWork.CustomerRepository.Add(customer);
                _applicationUnitOfWork.Save();
            }
            else
                throw new DuplicateCustomerNameException();
        }

        public void DeleteCustomer(Guid id)
        {
           _applicationUnitOfWork.CustomerRepository.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public Customer GetCustomer(Guid id)
        {
            return _applicationUnitOfWork.CustomerRepository.GetById(id);
        }

        public (IList<Customer> data, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return _applicationUnitOfWork.CustomerRepository.GetPagedCustomers(pageIndex, pageSize, order, search);
        }

        public void Update(Customer customer)
        {
           _applicationUnitOfWork.CustomerRepository.Update(customer);
            _applicationUnitOfWork.Save();
        }
    }
}
