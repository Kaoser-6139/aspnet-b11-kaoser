using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public  class Sale:IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int SerialNumber {  get; set; }
       // public Guid InvoiceNumber { get; set; }
        public DateTime Date {  get; set; }
        public string CustomerName { get; set; }
        public double Total {  get; set; }
        public double Paid {  get; set; }
        public double Due {  get; set; }
        public string Status {  get; set; }

    }
}
