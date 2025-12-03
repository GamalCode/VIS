namespace DataAccess.DAO
{
    public class User
    {
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime Created_At { get; set; }

        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            Role = string.Empty;
            Created_At = DateTime.Now;
        }

        public User(int user_ID, string username, string password, string role, DateTime created_At)
        {
            User_ID = user_ID;
            Username = username;
            Password = password;
            Role = role;
            Created_At = created_At;
        }
    }
}