using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.DefectiveProduct
{
    public class DefectiveProductSqlDAO : IDefectiveProductDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public DefectiveProductSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
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

        public List<DAO.DefectiveProduct> GetAll()
        {
            var list = new List<DAO.DefectiveProduct>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DAO.DefectiveProduct
                            {
                                Defective_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Report_Date = DateTime.Parse(reader.GetString(4)),
                                Reason = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return list;
        }

        public DAO.DefectiveProduct GetById(int id)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct WHERE Defective_ID = @Defective_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Defective_ID"; p.Value = id; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.DefectiveProduct
                            {
                                Defective_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Report_Date = DateTime.Parse(reader.GetString(4)),
                                Reason = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            };
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return null;
        }

        public int Insert(DAO.DefectiveProduct dp)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO DefectiveProduct (Product_ID, Storage_ID, Quantity, Report_Date, Reason) VALUES (@Product_ID, @Storage_ID, @Quantity, @Report_Date, @Reason); SELECT last_insert_rowid();";
                    var p1 = command.CreateParameter(); p1.ParameterName = "@Product_ID"; p1.Value = dp.Product_ID; command.Parameters.Add(p1);
                    var p2 = command.CreateParameter(); p2.ParameterName = "@Storage_ID"; p2.Value = dp.Storage_ID; command.Parameters.Add(p2);
                    var p3 = command.CreateParameter(); p3.ParameterName = "@Quantity"; p3.Value = dp.Quantity; command.Parameters.Add(p3);
                    var p4 = command.CreateParameter(); p4.ParameterName = "@Report_Date"; p4.Value = dp.Report_Date.ToString("yyyy-MM-dd HH:mm:ss"); command.Parameters.Add(p4);
                    var p5 = command.CreateParameter(); p5.ParameterName = "@Reason"; p5.Value = string.IsNullOrEmpty(dp.Reason) ? DBNull.Value : dp.Reason; command.Parameters.Add(p5);
                    var id = Convert.ToInt32(command.ExecuteScalar());
                    dp.Defective_ID = id;
                    return id;
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public void Update(DAO.DefectiveProduct dp)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE DefectiveProduct SET Product_ID = @Product_ID, Storage_ID = @Storage_ID, Quantity = @Quantity, Report_Date = @Report_Date, Reason = @Reason WHERE Defective_ID = @Defective_ID";
                    var p0 = command.CreateParameter(); p0.ParameterName = "@Defective_ID"; p0.Value = dp.Defective_ID; command.Parameters.Add(p0);
                    var p1 = command.CreateParameter(); p1.ParameterName = "@Product_ID"; p1.Value = dp.Product_ID; command.Parameters.Add(p1);
                    var p2 = command.CreateParameter(); p2.ParameterName = "@Storage_ID"; p2.Value = dp.Storage_ID; command.Parameters.Add(p2);
                    var p3 = command.CreateParameter(); p3.ParameterName = "@Quantity"; p3.Value = dp.Quantity; command.Parameters.Add(p3);
                    var p4 = command.CreateParameter(); p4.ParameterName = "@Report_Date"; p4.Value = dp.Report_Date.ToString("yyyy-MM-dd HH:mm:ss"); command.Parameters.Add(p4);
                    var p5 = command.CreateParameter(); p5.ParameterName = "@Reason"; p5.Value = string.IsNullOrEmpty(dp.Reason) ? DBNull.Value : dp.Reason; command.Parameters.Add(p5);
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
                    command.CommandText = "DELETE FROM DefectiveProduct WHERE Defective_ID = @Defective_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Defective_ID"; p.Value = id; command.Parameters.Add(p);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public List<DAO.DefectiveProduct> GetByStorage_ID(int storage_ID)
        {
            var list = new List<DAO.DefectiveProduct>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct WHERE Storage_ID = @Storage_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Storage_ID"; p.Value = storage_ID; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DAO.DefectiveProduct
                            {
                                Defective_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Report_Date = DateTime.Parse(reader.GetString(4)),
                                Reason = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return list;
        }

        public List<DAO.DefectiveProduct> GetByProduct_ID(int product_ID)
        {
            var list = new List<DAO.DefectiveProduct>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct WHERE Product_ID = @Product_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Product_ID"; p.Value = product_ID; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DAO.DefectiveProduct
                            {
                                Defective_ID = reader.GetInt32(0),
                                Product_ID = reader.GetInt32(1),
                                Storage_ID = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Report_Date = DateTime.Parse(reader.GetString(4)),
                                Reason = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return list;
        }
    }
}