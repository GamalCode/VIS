namespace DataAccess.DAO
{
    public class DefectiveProduct
    {
        public int Defective_ID { get; set; }
        public int Product_ID { get; set; }
        public int Storage_ID { get; set; }
        public int Quantity { get; set; }
        public DateTime Report_Date { get; set; }
        public string Reason { get; set; }

        public DefectiveProduct()
        {
            Report_Date = DateTime.Now;
            Reason = string.Empty;
        }

        public DefectiveProduct(int defective_ID, int product_ID, int storage_ID, int quantity, DateTime report_Date, string reason)
        {
            Defective_ID = defective_ID;
            Product_ID = product_ID;
            Storage_ID = storage_ID;
            Quantity = quantity;
            Report_Date = report_Date;
            Reason = reason;
        }
    }
}