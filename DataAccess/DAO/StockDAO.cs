namespace DataAccess.DAO
{
    public class Stock
    {
        public int Stock_ID { get; set; }
        public int Product_ID { get; set; }
        public int Storage_ID { get; set; }
        public int Quantity { get; set; }
        public string Location_In_Storage { get; set; }

        public Stock()
        {
            Location_In_Storage = string.Empty;
        }

        public Stock(int stock_ID, int product_ID, int storage_ID, int quantity, string location_In_Storage)
        {
            Stock_ID = stock_ID;
            Product_ID = product_ID;
            Storage_ID = storage_ID;
            Quantity = quantity;
            Location_In_Storage = location_In_Storage;
        }
    }
}