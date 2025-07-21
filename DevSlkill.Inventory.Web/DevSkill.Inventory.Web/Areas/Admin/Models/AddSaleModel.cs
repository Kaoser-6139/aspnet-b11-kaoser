namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class AddSaleModel
    {
        public int SerialNumber { get; set; }
       // public Guid InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public double Total { get; set; }
        public double Paid { get; set; }
        public double Due { get; set; }
        public string Status { get; set; }
    }
}
