namespace Domain.Models
{
    public class DefectiveProduct
    {
        public int Defective_ID { get; set; }
        public int Product_ID { get; set; }
        public int Storage_ID { get; set; }
        public int Quantity { get; set; }
        public DateTime Report_Date { get; set; }
        public string Reason { get; set; }
    }
}