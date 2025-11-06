namespace DataAccess.DAO
{
    public class Supplier
    {
        public int Supplier_ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Supplier()
        {
            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
        }

        public Supplier(int supplier_ID, string name, string phone, string email)
        {
            Supplier_ID = supplier_ID;
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
