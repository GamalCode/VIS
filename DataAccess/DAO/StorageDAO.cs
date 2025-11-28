namespace DataAccess.DAO
{
    public class Storage
    {
        public int Storage_ID { get; set; }
        public string Storage_Location { get; set; }
        public int Storage_Capacity { get; set; }
        public DateTime Last_Updated { get; set; }

        public Storage()
        {
            Storage_Location = string.Empty;
            Last_Updated = DateTime.Now;
        }

        public Storage(int storage_ID, string storage_Location, int storage_Capacity, DateTime last_Updated)
        {
            Storage_ID = storage_ID;
            Storage_Location = storage_Location;
            Storage_Capacity = storage_Capacity;
            Last_Updated = last_Updated;
        }
    }
}