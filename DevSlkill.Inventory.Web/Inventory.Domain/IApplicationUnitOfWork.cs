using Inventory.Domain;
using Inventory.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain
{
    public  interface IApplicationUnitOfWork:IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
    }
}
