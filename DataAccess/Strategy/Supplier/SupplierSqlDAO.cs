using DataAccess.Database;

namespace DataAccess.Strategy.Supplier
{
    public class SupplierSqlDAO : ISupplierDAO
    {
        private readonly DatabaseConnection _dbConnection;

        public SupplierSqlDAO(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<DAO.Supplier> GetAll()
        {
            var suppliers = new List<DAO.Supplier>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email FROM Supplier";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return suppliers;
        }

        public DAO.Supplier GetById(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email FROM Supplier WHERE Supplier_ID = @Supplier_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Supplier_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public int Insert(DAO.Supplier supplier)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Supplier (Name, Phone, Email, Storage_ID) 
                                  VALUES (@Name, @Phone, @Email, @Storage_ID);
                                  SELECT last_insert_rowid();";

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@Name";
                    paramName.Value = supplier.Name;
                    command.Parameters.Add(paramName);

                    var paramPhone = command.CreateParameter();
                    paramPhone.ParameterName = "@Phone";
                    paramPhone.Value = string.IsNullOrEmpty(supplier.Phone) ? DBNull.Value : supplier.Phone;
                    command.Parameters.Add(paramPhone);

                    var paramEmail = command.CreateParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = string.IsNullOrEmpty(supplier.Email) ? DBNull.Value : supplier.Email;
                    command.Parameters.Add(paramEmail);

                    var paramStorageID = command.CreateParameter();
                    paramStorageID.ParameterName = "@Storage_ID";
                    paramStorageID.Value = supplier.Storage_ID;
                    command.Parameters.Add(paramStorageID);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    supplier.Supplier_ID = id;
                    return id;
                }
            }
        }

        public void Update(DAO.Supplier supplier)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE Supplier 
                                          SET Name = @Name, Phone = @Phone, Email = @Email 
                                          WHERE Supplier_ID = @Supplier_ID";

                    var paramSupplier_ID = command.CreateParameter();
                    paramSupplier_ID.ParameterName = "@Supplier_ID";
                    paramSupplier_ID.Value = supplier.Supplier_ID;
                    command.Parameters.Add(paramSupplier_ID);

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@Name";
                    paramName.Value = supplier.Name;
                    command.Parameters.Add(paramName);

                    var paramPhone = command.CreateParameter();
                    paramPhone.ParameterName = "@Phone";
                    paramPhone.Value = string.IsNullOrEmpty(supplier.Phone) ? DBNull.Value : supplier.Phone;
                    command.Parameters.Add(paramPhone);

                    var paramEmail = command.CreateParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = string.IsNullOrEmpty(supplier.Email) ? DBNull.Value : supplier.Email;
                    command.Parameters.Add(paramEmail);

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
                    command.CommandText = "DELETE FROM Supplier WHERE Supplier_ID = @Supplier_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Supplier_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<DAO.Supplier> GetByName(string name)
        {
            var suppliers = new List<DAO.Supplier>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email FROM Supplier WHERE Name LIKE @Name";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Name";
                    parameter.Value = "%" + name + "%";
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new DAO.Supplier
                            {
                                Supplier_ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return suppliers;
        }

        public List<DAO.Supplier> GetByStorage_ID(int storage_ID)
        {
            var suppliers = new List<DAO.Supplier>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Supplier_ID, Name, Phone, Email, Storage_ID FROM Supplier WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
                    parameter.Value = storage_ID;
                    command.Parameters.Add(parameter);

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

            return suppliers;
        }
    }
}