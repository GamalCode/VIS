using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.Stock
{
    public class StockSqlDAO : IStockDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public StockSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
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

        public List<DAO.Stock> GetAll()
        {
            var stocks = new List<DAO.Stock>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Stock_ID, Product_ID, Storage_ID, Quantity, Location_In_Storage FROM Stock";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new DAO.Stock
                            {
                                Stock_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Location_In_Storage = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
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

            return stocks;
        }

        public DAO.Stock GetById(int id)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Stock_ID, Product_ID, Storage_ID, Quantity, Location_In_Storage FROM Stock WHERE Stock_ID = @Stock_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Stock_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Stock
                            {
                                Stock_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Location_In_Storage = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
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

        public int Insert(DAO.Stock stock)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO Stock (Product_ID, Storage_ID, Quantity, Location_In_Storage) 
                                          VALUES (@Product_ID, @Storage_ID, @Quantity, @Location_In_Storage);
                                          SELECT last_insert_rowid();";

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = stock.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramStorage_ID = command.CreateParameter();
                    paramStorage_ID.ParameterName = "@Storage_ID";
                    paramStorage_ID.Value = stock.Storage_ID;
                    command.Parameters.Add(paramStorage_ID);

                    var paramQuantity = command.CreateParameter();
                    paramQuantity.ParameterName = "@Quantity";
                    paramQuantity.Value = stock.Quantity;
                    command.Parameters.Add(paramQuantity);

                    var paramLocation = command.CreateParameter();
                    paramLocation.ParameterName = "@Location_In_Storage";
                    paramLocation.Value = string.IsNullOrEmpty(stock.Location_In_Storage) ? DBNull.Value : stock.Location_In_Storage;
                    command.Parameters.Add(paramLocation);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    stock.Stock_ID = id;
                    return id;
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }
        }

        public void Update(DAO.Stock stock)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE Stock 
                                          SET Product_ID = @Product_ID, Storage_ID = @Storage_ID, Quantity = @Quantity, Location_In_Storage = @Location_In_Storage 
                                          WHERE Stock_ID = @Stock_ID";

                    var paramStock_ID = command.CreateParameter();
                    paramStock_ID.ParameterName = "@Stock_ID";
                    paramStock_ID.Value = stock.Stock_ID;
                    command.Parameters.Add(paramStock_ID);

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = stock.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramStorage_ID = command.CreateParameter();
                    paramStorage_ID.ParameterName = "@Storage_ID";
                    paramStorage_ID.Value = stock.Storage_ID;
                    command.Parameters.Add(paramStorage_ID);

                    var paramQuantity = command.CreateParameter();
                    paramQuantity.ParameterName = "@Quantity";
                    paramQuantity.Value = stock.Quantity;
                    command.Parameters.Add(paramQuantity);

                    var paramLocation = command.CreateParameter();
                    paramLocation.ParameterName = "@Location_In_Storage";
                    paramLocation.Value = string.IsNullOrEmpty(stock.Location_In_Storage) ? DBNull.Value : stock.Location_In_Storage;
                    command.Parameters.Add(paramLocation);

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
                    command.CommandText = "DELETE FROM Stock WHERE Stock_ID = @Stock_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Stock_ID";
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

        public List<DAO.Stock> GetByProduct_ID(int product_ID)
        {
            var stocks = new List<DAO.Stock>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Stock_ID, Product_ID, Storage_ID, Quantity, Location_In_Storage FROM Stock WHERE Product_ID = @Product_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Product_ID";
                    parameter.Value = product_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new DAO.Stock
                            {
                                Stock_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Location_In_Storage = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
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

            return stocks;
        }

        public List<DAO.Stock> GetByStorage_ID(int storage_ID)
        {
            var stocks = new List<DAO.Stock>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Stock_ID, Product_ID, Storage_ID, Quantity, Location_In_Storage FROM Stock WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
                    parameter.Value = storage_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stocks.Add(new DAO.Stock
                            {
                                Stock_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Location_In_Storage = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
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

            return stocks;
        }
    }
}