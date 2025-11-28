namespace DataAccess.DAO
{
    public class Company
    {
        public int Company_ID { get; set; }
        public string Company_Name { get; set; }
        public string Contact_Email { get; set; }
        public string Contact_Phone { get; set; }
        public int Storage_ID { get; set; }

        public Company()
        {
            Company_Name = string.Empty;
            Contact_Email = string.Empty;
            Contact_Phone = string.Empty;
        }

        public Company(int company_ID, string company_Name, string contact_Email, string contact_Phone, int storage_ID)
        {
            Company_ID = company_ID;
            Company_Name = company_Name;
            Contact_Email = contact_Email;
            Contact_Phone = contact_Phone;
            Storage_ID = storage_ID;
        }
    }
}