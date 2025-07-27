using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Web.Application.Exceptions
{
    public  class DuplicateCustomerNameException:Exception
    {
        public DuplicateCustomerNameException() 
            :base("Customer name can't be duplicated")
        { 

        }
       

    }
}
 