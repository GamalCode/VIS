namespace Domain.Models
{
    public class Product
    {
        public int Product_ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string CarModel { get; set; }
        public int Supplier_ID { get; set; }
        public decimal Price { get; set; }
        public int Storage_ID { get; set; }
    }
}