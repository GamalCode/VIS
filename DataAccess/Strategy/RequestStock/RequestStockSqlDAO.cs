using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.RequestStock
{
    public class RequestStockSqlDAO : IRequestStockDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public RequestStockSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
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

        public List<DAO.RequestStock> GetAll()
        {
            var requestStocks = new List<DAO.RequestStock>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_Stock_ID, Request_ID, Stock_ID, Allocated_Quantity FROM RequestStock";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requestStocks.Add(new DAO.RequestStock
                            {
                                Request_Stock_ID = reader.GetInt32(0),
                                Request_ID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Stock_ID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Allocated_Quantity = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)
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

            return requestStocks;
        }

        public DAO.RequestStock GetById(int id)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_Stock_ID, Request_ID, Stock_ID, Allocated_Quantity FROM RequestStock WHERE Request_Stock_ID = @Request_Stock_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Request_Stock_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.RequestStock
                            {
                                Request_Stock_ID = reader.GetInt32(0),
                                Request_ID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Stock_ID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Allocated_Quantity = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)
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

        public int Insert(DAO.RequestStock requestStock)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO RequestStock (Request_ID, Stock_ID, Allocated_Quantity) 
                                          VALUES (@Request_ID, @Stock_ID, @Allocated_Quantity);
                                          SELECT last_insert_rowid();";

                    var paramRequest_ID = command.CreateParameter();
                    paramRequest_ID.ParameterName = "@Request_ID";
                    paramRequest_ID.Value = requestStock.Request_ID.HasValue ? (object)requestStock.Request_ID.Value : DBNull.Value;
                    command.Parameters.Add(paramRequest_ID);

                    var paramStock_ID = command.CreateParameter();
                    paramStock_ID.ParameterName = "@Stock_ID";
                    paramStock_ID.Value = requestStock.Stock_ID.HasValue ? (object)requestStock.Stock_ID.Value : DBNull.Value;
                    command.Parameters.Add(paramStock_ID);

                    var paramAllocated = command.CreateParameter();
                    paramAllocated.ParameterName = "@Allocated_Quantity";
                    paramAllocated.Value = requestStock.Allocated_Quantity.HasValue ? (object)requestStock.Allocated_Quantity.Value : DBNull.Value;
                    command.Parameters.Add(paramAllocated);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    requestStock.Request_Stock_ID = id;
                    return id;
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }
        }

        public void Update(DAO.RequestStock requestStock)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE RequestStock 
                                          SET Request_ID = @Request_ID, Stock_ID = @Stock_ID, Allocated_Quantity = @Allocated_Quantity 
                                          WHERE Request_Stock_ID = @Request_Stock_ID";

                    var paramRequest_Stock_ID = command.CreateParameter();
                    paramRequest_Stock_ID.ParameterName = "@Request_Stock_ID";
                    paramRequest_Stock_ID.Value = requestStock.Request_Stock_ID;
                    command.Parameters.Add(paramRequest_Stock_ID);

                    var paramRequest_ID = command.CreateParameter();
                    paramRequest_ID.ParameterName = "@Request_ID";
                    paramRequest_ID.Value = requestStock.Request_ID.HasValue ? (object)requestStock.Request_ID.Value : DBNull.Value;
                    command.Parameters.Add(paramRequest_ID);

                    var paramStock_ID = command.CreateParameter();
                    paramStock_ID.ParameterName = "@Stock_ID";
                    paramStock_ID.Value = requestStock.Stock_ID.HasValue ? (object)requestStock.Stock_ID.Value : DBNull.Value;
                    command.Parameters.Add(paramStock_ID);

                    var paramAllocated = command.CreateParameter();
                    paramAllocated.ParameterName = "@Allocated_Quantity";
                    paramAllocated.Value = requestStock.Allocated_Quantity.HasValue ? (object)requestStock.Allocated_Quantity.Value : DBNull.Value;
                    command.Parameters.Add(paramAllocated);

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
                    command.CommandText = "DELETE FROM RequestStock WHERE Request_Stock_ID = @Request_Stock_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Request_Stock_ID";
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

        public List<DAO.RequestStock> GetByRequest_ID(int request_ID)
        {
            var requestStocks = new List<DAO.RequestStock>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_Stock_ID, Request_ID, Stock_ID, Allocated_Quantity FROM RequestStock WHERE Request_ID = @Request_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Request_ID";
                    parameter.Value = request_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requestStocks.Add(new DAO.RequestStock
                            {
                                Request_Stock_ID = reader.GetInt32(0),
                                Request_ID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Stock_ID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Allocated_Quantity = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)
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

            return requestStocks;
        }

        public List<DAO.RequestStock> GetByStock_ID(int stock_ID)
        {
            var requestStocks = new List<DAO.RequestStock>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Request_Stock_ID, Request_ID, Stock_ID, Allocated_Quantity FROM RequestStock WHERE Stock_ID = @Stock_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Stock_ID";
                    parameter.Value = stock_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            requestStocks.Add(new DAO.RequestStock
                            {
                                Request_Stock_ID = reader.GetInt32(0),
                                Request_ID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                Stock_ID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Allocated_Quantity = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)
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

            return requestStocks;
        }
    }
}