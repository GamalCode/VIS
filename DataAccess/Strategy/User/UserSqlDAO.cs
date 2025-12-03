using DataAccess.Database;

namespace DataAccess.Strategy.User
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly DatabaseConnection _dbConnection;

        public UserSqlDAO(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public DAO.User? GetByUsername(string username)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT User_ID, Username, Password, Role, Created_At FROM User WHERE Username = @Username";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Username";
                    parameter.Value = username;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.User
                            {
                                User_ID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3),
                                Created_At = DateTime.Parse(reader.GetString(4))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public DAO.User? ValidateUser(string username, string password)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT User_ID, Username, Password, Role, Created_At FROM User WHERE Username = @Username AND Password = @Password";

                    var paramUsername = command.CreateParameter();
                    paramUsername.ParameterName = "@Username";
                    paramUsername.Value = username;
                    command.Parameters.Add(paramUsername);

                    var paramPassword = command.CreateParameter();
                    paramPassword.ParameterName = "@Password";
                    paramPassword.Value = password;
                    command.Parameters.Add(paramPassword);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.User
                            {
                                User_ID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3),
                                Created_At = DateTime.Parse(reader.GetString(4))
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}