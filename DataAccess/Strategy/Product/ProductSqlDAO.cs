using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.Product
{
    public class ProductSqlDAO : IProductDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public ProductSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
        {
            _dbConnection = dbConnection;
            _unitOfWork = unitOfWork;
        }

        private IDbConnection GetConnection()
        {
            if (_unitOfWork != null) return _unitOfWork.Connection;
            var conn = _dbConnection.CreateConnection();
            conn.Open();
            return conn;
        }

        private bool ShouldDisposeConnection => _unitOfWork == null;

        public List<DAO.Product> GetAll()
        {
            var products = new List<DAO.Product>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new DAO.Product
                            {
                                Product_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                CarModel = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Supplier_ID = reader.GetInt32(4),
                                Price = (decimal)reader.GetDouble(5),
                                Storage_ID = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return products;
        }

        public DAO.Product GetById(int id)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product WHERE Product_ID = @Product_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Product_ID"; p.Value = id; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Product
                            {
                                Product_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                CarModel = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Supplier_ID = reader.GetInt32(4),
                                Price = (decimal)reader.GetDouble(5),
                                Storage_ID = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return null;
        }

        public int Insert(DAO.Product product)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO Product (Name, Type, CarModel, Supplier_ID, Price, Storage_ID) VALUES (@Name, @Type, @CarModel, @Supplier_ID, @Price, @Storage_ID); SELECT last_insert_rowid();";
                    var p1 = command.CreateParameter(); p1.ParameterName = "@Name"; p1.Value = product.Name; command.Parameters.Add(p1);
                    var p2 = command.CreateParameter(); p2.ParameterName = "@Type"; p2.Value = string.IsNullOrEmpty(product.Type) ? DBNull.Value : product.Type; command.Parameters.Add(p2);
                    var p3 = command.CreateParameter(); p3.ParameterName = "@CarModel"; p3.Value = string.IsNullOrEmpty(product.CarModel) ? DBNull.Value : product.CarModel; command.Parameters.Add(p3);
                    var p4 = command.CreateParameter(); p4.ParameterName = "@Supplier_ID"; p4.Value = product.Supplier_ID; command.Parameters.Add(p4);
                    var p5 = command.CreateParameter(); p5.ParameterName = "@Price"; p5.Value = (double)product.Price; command.Parameters.Add(p5);
                    var p6 = command.CreateParameter(); p6.ParameterName = "@Storage_ID"; p6.Value = product.Storage_ID; command.Parameters.Add(p6);
                    var id = Convert.ToInt32(command.ExecuteScalar());
                    product.Product_ID = id;
                    return id;
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public void Update(DAO.Product product)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE Product SET Name = @Name, Type = @Type, CarModel = @CarModel, Supplier_ID = @Supplier_ID, Price = @Price, Storage_ID = @Storage_ID WHERE Product_ID = @Product_ID";
                    var p0 = command.CreateParameter(); p0.ParameterName = "@Product_ID"; p0.Value = product.Product_ID; command.Parameters.Add(p0);
                    var p1 = command.CreateParameter(); p1.ParameterName = "@Name"; p1.Value = product.Name; command.Parameters.Add(p1);
                    var p2 = command.CreateParameter(); p2.ParameterName = "@Type"; p2.Value = string.IsNullOrEmpty(product.Type) ? DBNull.Value : product.Type; command.Parameters.Add(p2);
                    var p3 = command.CreateParameter(); p3.ParameterName = "@CarModel"; p3.Value = string.IsNullOrEmpty(product.CarModel) ? DBNull.Value : product.CarModel; command.Parameters.Add(p3);
                    var p4 = command.CreateParameter(); p4.ParameterName = "@Supplier_ID"; p4.Value = product.Supplier_ID; command.Parameters.Add(p4);
                    var p5 = command.CreateParameter(); p5.ParameterName = "@Price"; p5.Value = (double)product.Price; command.Parameters.Add(p5);
                    var p6 = command.CreateParameter(); p6.ParameterName = "@Storage_ID"; p6.Value = product.Storage_ID; command.Parameters.Add(p6);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public void Delete(int id)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "DELETE FROM Product WHERE Product_ID = @Product_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Product_ID"; p.Value = id; command.Parameters.Add(p);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public List<DAO.Product> GetBySupplier_ID(int supplier_ID)
        {
            var products = new List<DAO.Product>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product WHERE Supplier_ID = @Supplier_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Supplier_ID"; p.Value = supplier_ID; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new DAO.Product
                            {
                                Product_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                CarModel = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Supplier_ID = reader.GetInt32(4),
                                Price = (decimal)reader.GetDouble(5),
                                Storage_ID = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return products;
        }

        public List<DAO.Product> GetByStorage_ID(int storage_ID)
        {
            var products = new List<DAO.Product>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product WHERE Storage_ID = @Storage_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Storage_ID"; p.Value = storage_ID; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new DAO.Product
                            {
                                Product_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                CarModel = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Supplier_ID = reader.GetInt32(4),
                                Price = (decimal)reader.GetDouble(5),
                                Storage_ID = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return products;
        }
    }
}