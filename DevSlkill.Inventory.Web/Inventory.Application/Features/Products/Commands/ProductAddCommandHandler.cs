using Inventory.Domain;
using Inventory.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Products.Commands
{
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public ProductAddCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;

        }

        public async Task Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            await _applicationUnitOfWork.ProductRepository.AddAsync(new Product
            { 
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            });
            await _applicationUnitOfWork.SaveAsync();
           
        }
    }
}
