namespace Domain.Models
{
    public class RequestStock
    {
        public int Request_Stock_ID { get; set; }
        public int? Request_ID { get; set; }
        public int? Stock_ID { get; set; }
        public int? Allocated_Quantity { get; set; }
    }
}