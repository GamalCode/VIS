namespace Domain.Models
{
    public class Request
    {
        public int Request_ID { get; set; }
        public int Company_ID { get; set; }
        public int Product_ID { get; set; }
        public int Request_Quantity { get; set; }
        public DateTime Request_Date { get; set; }
        public string Status { get; set; }
    }
}