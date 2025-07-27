using DevSkill.Inventory.Web.Domain;
using Inventory.Domain.Entities;
using Inventory.Domain.Features.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Products.Queries
{
    public class GetProductsQuery:DataTables,IRequest<(IList<Product>,int,int)>,IGetProductsQuery
    {
    }
}
