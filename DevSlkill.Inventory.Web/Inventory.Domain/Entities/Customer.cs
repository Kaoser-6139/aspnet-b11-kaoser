using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Customer : IEntity<Guid>
    {
       
        public Guid Id { get; set; }
        public int SerialNumber {  get; set; }
        public string Name { get; set; }
        public string Mobile {  get; set; }
        public string Address { get; set; }
        public string Email {  get; set; }
        public double CurrentBalance {  get; set; }
        public string Status {  get; set; }

    }
}
