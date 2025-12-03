namespace DataAccess.Strategy.User
{
    public interface IUserDAO
    {
        DAO.User? GetByUsername(string username);
        DAO.User? ValidateUser(string username, string password);
    }
}