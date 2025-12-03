using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.User
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public UserSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
        {
            _dbConnection = dbConnection;
            _unitOfWork = unitOfWork;
        }

        private IDbConnection GetConnection()
        {
            if (_unitOfWork != null)
            {
                return _unitOfWork.Connection;
            }
            var conn = _dbConnection.CreateConnection();
            conn.Open();
            return conn;
        }

        private bool ShouldDisposeConnection => _unitOfWork == null;

        public DAO.User? GetByUsername(string username)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
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
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }

            return null;
        }

        public DAO.User? ValidateUser(string username, string password)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
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
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }

            return null;
        }
    }
}