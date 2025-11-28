namespace DataAccess.DAO
{
    public class RequestStock
    {
        public int Request_Stock_ID { get; set; }
        public int? Request_ID { get; set; }
        public int? Stock_ID { get; set; }
        public int? Allocated_Quantity { get; set; }

        public RequestStock()
        {
        }

        public RequestStock(int request_Stock_ID, int? request_ID, int? stock_ID, int? allocated_Quantity)
        {
            Request_Stock_ID = request_Stock_ID;
            Request_ID = request_ID;
            Stock_ID = stock_ID;
            Allocated_Quantity = allocated_Quantity;
        }
    }
}