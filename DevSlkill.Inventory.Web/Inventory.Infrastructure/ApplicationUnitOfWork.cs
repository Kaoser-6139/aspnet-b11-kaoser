using Inventory.Domain;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public  class ApplicationUnitOfWork:UnitOfWork,IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext context,
            IProductRepository productRepository) : base(context)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; private set; }

    }
}
