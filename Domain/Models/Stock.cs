namespace Domain.Models
{
    public class Stock
    {
        public int Stock_ID { get; set; }
        public int Product_ID { get; set; }
        public int Storage_ID { get; set; }
        public int Quantity { get; set; }
        public string Location_In_Storage { get; set; }
    }
}