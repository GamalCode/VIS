using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.Request
{
    public class RequestSqlDAO : IRequestDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public RequestSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
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

        public List<DAO.Request> GetAll()
        {
            var requests = new List<DAO.Request>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_ID, Company_ID, Product_ID, Request_Quantity, Request_Date, Status FROM Request";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requests.Add(new DAO.Request
                            {
                                Request_ID = reader.GetInt32(0),
                                Company_ID = reader.GetInt32(1),
                                Product_ID = reader.GetInt32(2),
                                Request_Quantity = reader.GetInt32(3),
                                Request_Date = DateTime.Parse(reader.GetString(4)),
                                Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }

            return requests;
        }

        public DAO.Request GetById(int id)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_ID, Company_ID, Product_ID, Request_Quantity, Request_Date, Status FROM Request WHERE Request_ID = @Request_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Request_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Request
                            {
                                Request_ID = reader.GetInt32(0),
                                Company_ID = reader.GetInt32(1),
                                Product_ID = reader.GetInt32(2),
                                Request_Quantity = reader.GetInt32(3),
                                Request_Date = DateTime.Parse(reader.GetString(4)),
                                Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
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

        public int Insert(DAO.Request request)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO Request (Company_ID, Product_ID, Request_Quantity, Request_Date, Status) 
                                          VALUES (@Company_ID, @Product_ID, @Request_Quantity, @Request_Date, @Status);
                                          SELECT last_insert_rowid();";

                    var paramCompany_ID = command.CreateParameter();
                    paramCompany_ID.ParameterName = "@Company_ID";
                    paramCompany_ID.Value = request.Company_ID;
                    command.Parameters.Add(paramCompany_ID);

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = request.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramQuantity = command.CreateParameter();
                    paramQuantity.ParameterName = "@Request_Quantity";
                    paramQuantity.Value = request.Request_Quantity;
                    command.Parameters.Add(paramQuantity);

                    var paramDate = command.CreateParameter();
                    paramDate.ParameterName = "@Request_Date";
                    paramDate.Value = request.Request_Date.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.Add(paramDate);

                    var paramStatus = command.CreateParameter();
                    paramStatus.ParameterName = "@Status";
                    paramStatus.Value = string.IsNullOrEmpty(request.Status) ? DBNull.Value : request.Status;
                    command.Parameters.Add(paramStatus);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    request.Request_ID = id;
                    return id;
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }
        }

        public void Update(DAO.Request request)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE Request 
                                          SET Company_ID = @Company_ID, Product_ID = @Product_ID, Request_Quantity = @Request_Quantity, Request_Date = @Request_Date, Status = @Status 
                                          WHERE Request_ID = @Request_ID";

                    var paramRequest_ID = command.CreateParameter();
                    paramRequest_ID.ParameterName = "@Request_ID";
                    paramRequest_ID.Value = request.Request_ID;
                    command.Parameters.Add(paramRequest_ID);

                    var paramCompany_ID = command.CreateParameter();
                    paramCompany_ID.ParameterName = "@Company_ID";
                    paramCompany_ID.Value = request.Company_ID;
                    command.Parameters.Add(paramCompany_ID);

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = request.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramQuantity = command.CreateParameter();
                    paramQuantity.ParameterName = "@Request_Quantity";
                    paramQuantity.Value = request.Request_Quantity;
                    command.Parameters.Add(paramQuantity);

                    var paramDate = command.CreateParameter();
                    paramDate.ParameterName = "@Request_Date";
                    paramDate.Value = request.Request_Date.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.Add(paramDate);

                    var paramStatus = command.CreateParameter();
                    paramStatus.ParameterName = "@Status";
                    paramStatus.Value = string.IsNullOrEmpty(request.Status) ? DBNull.Value : request.Status;
                    command.Parameters.Add(paramStatus);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }
        }

        public void Delete(int id)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "DELETE FROM Request WHERE Request_ID = @Request_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Request_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }
        }

        public List<DAO.Request> GetByCompany_ID(int company_ID)
        {
            var requests = new List<DAO.Request>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_ID, Company_ID, Product_ID, Request_Quantity, Request_Date, Status FROM Request WHERE Company_ID = @Company_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Company_ID";
                    parameter.Value = company_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requests.Add(new DAO.Request
                            {
                                Request_ID = reader.GetInt32(0),
                                Company_ID = reader.GetInt32(1),
                                Product_ID = reader.GetInt32(2),
                                Request_Quantity = reader.GetInt32(3),
                                Request_Date = DateTime.Parse(reader.GetString(4)),
                                Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }

            return requests;
        }

        public List<DAO.Request> GetByStatus(string status)
        {
            var requests = new List<DAO.Request>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_ID, Company_ID, Product_ID, Request_Quantity, Request_Date, Status FROM Request WHERE Status = @Status";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Status";
                    parameter.Value = status;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requests.Add(new DAO.Request
                            {
                                Request_ID = reader.GetInt32(0),
                                Company_ID = reader.GetInt32(1),
                                Product_ID = reader.GetInt32(2),
                                Request_Quantity = reader.GetInt32(3),
                                Request_Date = DateTime.Parse(reader.GetString(4)),
                                Status = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }

            return requests;
        }
    }
}