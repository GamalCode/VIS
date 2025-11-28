namespace DataAccess.DAO
{
    public class Supplier
    {
        public int Supplier_ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Storage_ID { get; set; }

        public Supplier()
        {
            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
        }

        public Supplier(int supplier_ID, string name, string phone, string email, int storage_ID)
        {
            Supplier_ID = supplier_ID;
            Name = name;
            Phone = phone;
            Email = email;
            Storage_ID = storage_ID;
        }
    }
}