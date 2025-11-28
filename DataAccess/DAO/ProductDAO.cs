namespace DataAccess.DAO
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

        public Product()
        {
            Name = string.Empty;
            Type = string.Empty;
            CarModel = string.Empty;
            Price = 0;
        }

        public Product(int product_ID, string name, string type, string carModel, int supplier_ID, decimal price, int storage_ID)
        {
            Product_ID = product_ID;
            Name = name;
            Type = type;
            CarModel = carModel;
            Supplier_ID = supplier_ID;
            Price = price;
            Storage_ID = storage_ID;
        }
    }
}