using DataAccess.GlobalConfig;
using Domain.Session;

namespace Domain.Services
{
    public class AuthService
    {
        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var userDAO = GlobalConfig.Connection.GetUserDAO();
            var user = userDAO.ValidateUser(username, password);

            if (user != null)
            {
                UserSession.Login(user.User_ID, user.Username, user.Role);
                return true;
            }

            return false;
        }

        public void Logout()
        {
            UserSession.Logout();
        }

        public bool IsLoggedIn()
        {
            return UserSession.IsLoggedIn;
        }

        public string? GetCurrentUsername()
        {
            return UserSession.Username;
        }

        public string? GetCurrentRole()
        {
            return UserSession.Role;
        }

        public bool IsAdmin()
        {
            return UserSession.IsAdmin();
        }

        public bool IsUser()
        {
            return UserSession.IsUser();
        }
    }
}