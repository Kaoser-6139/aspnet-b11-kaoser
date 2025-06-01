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
    public class GetUpdatedProductByIdHandler : IRequestHandler<GetUpdatedProductById,Product>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public GetUpdatedProductByIdHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<Product> Handle(GetUpdatedProductById request, CancellationToken cancellationToken)
        {
           var product=await _applicationUnitOfWork.ProductRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new ArgumentException("Product Not Found");
            }
            return product;
        }
    }
}
