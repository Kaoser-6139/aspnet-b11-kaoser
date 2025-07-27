//using Demo.Domain;
using DevSkill.Inventory.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.Products.Commands
{
    public  class ProductListModel:DataTables
    {
        public ProductSearchModel SearchItem { get; set; }
    }
}
