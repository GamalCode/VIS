using System.Text.Json;

namespace DataAccess.Strategy.User
{
    public class UserTextDAO : IUserDAO
    {
        private readonly string _filePath;

        public UserTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "users.json");
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_filePath))
            {
                var defaultUsers = new List<DAO.User>
                {
                    new DAO.User(1, "admin", "heslo", "admin", DateTime.Now),
                    new DAO.User(2, "user", "heslo", "user", DateTime.Now)
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(defaultUsers, options);
                File.WriteAllText(_filePath, json);
            }
        }

        private List<DAO.User> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.User>>(json) ?? new List<DAO.User>();
        }

        public DAO.User? GetByUsername(string username)
        {
            var users = ReadAll();
            return users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public DAO.User? ValidateUser(string username, string password)
        {
            var users = ReadAll();
            return users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}