namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UpdateBalanceTransferModel
    {
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public double TransferAmount { get; set; }
        public string? Note { get; set; }
    }
}
