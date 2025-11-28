namespace DataAccess.DAO
{
    public class Request
    {
        public int Request_ID { get; set; }
        public int Company_ID { get; set; }
        public int Product_ID { get; set; }
        public int Request_Quantity { get; set; }
        public DateTime Request_Date { get; set; }
        public string Status { get; set; }

        public Request()
        {
            Request_Date = DateTime.Now;
            Status = string.Empty;
        }

        public Request(int request_ID, int company_ID, int product_ID, int request_Quantity, DateTime request_Date, string status)
        {
            Request_ID = request_ID;
            Company_ID = company_ID;
            Product_ID = product_ID;
            Request_Quantity = request_Quantity;
            Request_Date = request_Date;
            Status = status;
        }
    }
}