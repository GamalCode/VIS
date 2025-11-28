using DataAccess.Database;

namespace DataAccess.Strategy.Request
{
    public class RequestSqlDAO : IRequestDAO
    {
        private readonly DatabaseConnection _dbConnection;

        public RequestSqlDAO(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<DAO.Request> GetAll()
        {
            var requests = new List<DAO.Request>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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

            return requests;
        }

        public DAO.Request GetById(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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

            return null;
        }

        public int Insert(DAO.Request request)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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
        }

        public void Update(DAO.Request request)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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
        }

        public void Delete(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Request WHERE Request_ID = @Request_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Request_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<DAO.Request> GetByCompany_ID(int company_ID)
        {
            var requests = new List<DAO.Request>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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

            return requests;
        }

        public List<DAO.Request> GetByStatus(string status)
        {
            var requests = new List<DAO.Request>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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

            return requests;
        }
    }
}