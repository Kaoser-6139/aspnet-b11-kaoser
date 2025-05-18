using Inventory.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Products.Commands
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public ProductDeleteCommandHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _applicationUnitOfWork.ProductRepository.GetByIdAsync(request.Id);
            _applicationUnitOfWork.ProductRepository.Remove(product);
             await  _applicationUnitOfWork.SaveAsync();
        }
    }
}
