using Inventory.Domain;
using Inventory.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, (IList<Product>, int, int)>
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public GetProductQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task<(IList<Product>, int, int)> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.ProductRepository.GetPagedProductsAsync(request);
        }
    }
}
