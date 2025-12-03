namespace Domain.Session
{
    public static class UserSession
    {
        public static int? UserId { get; private set; }
        public static string? Username { get; private set; }
        public static string? Role { get; private set; }
        public static DateTime? LoginTime { get; private set; }
        public static bool IsLoggedIn => UserId.HasValue;

        public static void Login(int userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
            LoginTime = DateTime.Now;
        }

        public static void Logout()
        {
            UserId = null;
            Username = null;
            Role = null;
            LoginTime = null;
        }

        public static bool IsAdmin()
        {
            return IsLoggedIn && Role?.ToLower() == "admin";
        }

        public static bool IsUser()
        {
            return IsLoggedIn && Role?.ToLower() == "user";
        }
    }
}