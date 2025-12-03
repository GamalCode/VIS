using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.Supplier
{
    public class SupplierSqlDAO : ISupplierDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public SupplierSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
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

        public List<DAO.Supplier> GetAll()
        {
            var suppliers = new List<DAO.Supplier>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email, Storage_ID FROM Supplier";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return suppliers;
        }

        public DAO.Supplier GetById(int id)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email, Storage_ID FROM Supplier WHERE Supplier_ID = @Supplier_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Supplier_ID"; p.Value = id; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return null;
        }

        public int Insert(DAO.Supplier supplier)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO Supplier (Name, Phone, Email, Storage_ID) VALUES (@Name, @Phone, @Email, @Storage_ID); SELECT last_insert_rowid();";
                    var p1 = command.CreateParameter(); p1.ParameterName = "@Name"; p1.Value = supplier.Name; command.Parameters.Add(p1);
                    var p2 = command.CreateParameter(); p2.ParameterName = "@Phone"; p2.Value = string.IsNullOrEmpty(supplier.Phone) ? DBNull.Value : supplier.Phone; command.Parameters.Add(p2);
                    var p3 = command.CreateParameter(); p3.ParameterName = "@Email"; p3.Value = string.IsNullOrEmpty(supplier.Email) ? DBNull.Value : supplier.Email; command.Parameters.Add(p3);
                    var p4 = command.CreateParameter(); p4.ParameterName = "@Storage_ID"; p4.Value = supplier.Storage_ID; command.Parameters.Add(p4);
                    var id = Convert.ToInt32(command.ExecuteScalar());
                    supplier.Supplier_ID = id;
                    return id;
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public void Update(DAO.Supplier supplier)
        {
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE Supplier SET Name = @Name, Phone = @Phone, Email = @Email, Storage_ID = @Storage_ID WHERE Supplier_ID = @Supplier_ID";
                    var p0 = command.CreateParameter(); p0.ParameterName = "@Supplier_ID"; p0.Value = supplier.Supplier_ID; command.Parameters.Add(p0);
                    var p1 = command.CreateParameter(); p1.ParameterName = "@Name"; p1.Value = supplier.Name; command.Parameters.Add(p1);
                    var p2 = command.CreateParameter(); p2.ParameterName = "@Phone"; p2.Value = string.IsNullOrEmpty(supplier.Phone) ? DBNull.Value : supplier.Phone; command.Parameters.Add(p2);
                    var p3 = command.CreateParameter(); p3.ParameterName = "@Email"; p3.Value = string.IsNullOrEmpty(supplier.Email) ? DBNull.Value : supplier.Email; command.Parameters.Add(p3);
                    var p4 = command.CreateParameter(); p4.ParameterName = "@Storage_ID"; p4.Value = supplier.Storage_ID; command.Parameters.Add(p4);
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
                    command.CommandText = "DELETE FROM Supplier WHERE Supplier_ID = @Supplier_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Supplier_ID"; p.Value = id; command.Parameters.Add(p);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
        }

        public List<DAO.Supplier> GetByName(string name)
        {
            var suppliers = new List<DAO.Supplier>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email, Storage_ID FROM Supplier WHERE Name LIKE @Name";
                    var p = command.CreateParameter(); p.ParameterName = "@Name"; p.Value = "%" + name + "%"; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return suppliers;
        }

        public List<DAO.Supplier> GetByStorage_ID(int storage_ID)
        {
            var suppliers = new List<DAO.Supplier>();
            var connection = GetConnection();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email, Storage_ID FROM Supplier WHERE Storage_ID = @Storage_ID";
                    var p = command.CreateParameter(); p.ParameterName = "@Storage_ID"; p.Value = storage_ID; command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            finally { if (ShouldDisposeConnection) connection.Dispose(); }
            return suppliers;
        }
    }
}