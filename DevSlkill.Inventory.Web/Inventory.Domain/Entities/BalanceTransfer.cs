using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class BalanceTransfer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int SerialNumber {  get; set; }
        public DateTime Date { get; set; }
        public int FromAccount {  get; set; }   
        public int ToAccount { get; set; } 
        public double TransferAmount {  get; set; }
        public string Note {  get; set; }

    }
}
