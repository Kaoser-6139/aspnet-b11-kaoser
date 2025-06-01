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
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public ProductUpdateCommandHandler(IApplicationUnitOfWork applicationUnitOfWork) 
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            _applicationUnitOfWork.ProductRepository.Update(product);
            await _applicationUnitOfWork.SaveAsync();
        }
        
    }
}
