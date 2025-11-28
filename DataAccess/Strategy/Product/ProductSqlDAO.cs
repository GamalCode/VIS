using DataAccess.Database;

namespace DataAccess.Strategy.Product
{
    public class ProductSqlDAO : IProductDAO
    {
        private readonly DatabaseConnection _dbConnection;

        public ProductSqlDAO(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<DAO.Product> GetAll()
        {
            var products = new List<DAO.Product>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
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

            return products;
        }

        public DAO.Product GetById(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product WHERE Product_ID = @Product_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Product_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

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

            return null;
        }

        public int Insert(DAO.Product product)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Product (Name, Type, CarModel, Supplier_ID, Price, Storage_ID) 
                                          VALUES (@Name, @Type, @CarModel, @Supplier_ID, @Price, @Storage_ID);
                                          SELECT last_insert_rowid();";

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@Name";
                    paramName.Value = product.Name;
                    command.Parameters.Add(paramName);

                    var paramType = command.CreateParameter();
                    paramType.ParameterName = "@Type";
                    paramType.Value = string.IsNullOrEmpty(product.Type) ? DBNull.Value : product.Type;
                    command.Parameters.Add(paramType);

                    var paramCarModel = command.CreateParameter();
                    paramCarModel.ParameterName = "@CarModel";
                    paramCarModel.Value = string.IsNullOrEmpty(product.CarModel) ? DBNull.Value : product.CarModel;
                    command.Parameters.Add(paramCarModel);

                    var paramSupplier_ID = command.CreateParameter();
                    paramSupplier_ID.ParameterName = "@Supplier_ID";
                    paramSupplier_ID.Value = product.Supplier_ID;
                    command.Parameters.Add(paramSupplier_ID);

                    var paramPrice = command.CreateParameter();
                    paramPrice.ParameterName = "@Price";
                    paramPrice.Value = (double)product.Price;
                    command.Parameters.Add(paramPrice);

                    var paramStorageID = command.CreateParameter();
                    paramStorageID.ParameterName = "@Storage_ID";
                    paramStorageID.Value = product.Storage_ID;
                    command.Parameters.Add(paramStorageID);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    product.Product_ID = id;
                    return id;
                }
            }
        }

        public void Update(DAO.Product product)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE Product 
                                          SET Name = @Name, Type = @Type, CarModel = @CarModel, Supplier_ID = @Supplier_ID, Price = @Price, Storage_ID = @Storage_ID 
                                          WHERE Product_ID = @Product_ID";

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = product.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@Name";
                    paramName.Value = product.Name;
                    command.Parameters.Add(paramName);

                    var paramType = command.CreateParameter();
                    paramType.ParameterName = "@Type";
                    paramType.Value = string.IsNullOrEmpty(product.Type) ? DBNull.Value : product.Type;
                    command.Parameters.Add(paramType);

                    var paramCarModel = command.CreateParameter();
                    paramCarModel.ParameterName = "@CarModel";
                    paramCarModel.Value = string.IsNullOrEmpty(product.CarModel) ? DBNull.Value : product.CarModel;
                    command.Parameters.Add(paramCarModel);

                    var paramSupplier_ID = command.CreateParameter();
                    paramSupplier_ID.ParameterName = "@Supplier_ID";
                    paramSupplier_ID.Value = product.Supplier_ID;
                    command.Parameters.Add(paramSupplier_ID);

                    var paramPrice = command.CreateParameter();
                    paramPrice.ParameterName = "@Price";
                    paramPrice.Value = (double)product.Price;
                    command.Parameters.Add(paramPrice);

                    var paramStorageID = command.CreateParameter();
                    paramStorageID.ParameterName = "@Storage_ID";
                    paramStorageID.Value = product.Storage_ID;
                    command.Parameters.Add(paramStorageID);

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
                    command.CommandText = "DELETE FROM Product WHERE Product_ID = @Product_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Product_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<DAO.Product> GetBySupplier_ID(int supplier_ID)
        {
            var products = new List<DAO.Product>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product WHERE Supplier_ID = @Supplier_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Supplier_ID";
                    parameter.Value = supplier_ID;
                    command.Parameters.Add(parameter);

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

            return products;
        }

        public List<DAO.Product> GetByStorage_ID(int storage_ID)
        {
            var products = new List<DAO.Product>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Product_ID, Name, Type, CarModel, Supplier_ID, Price, Storage_ID FROM Product WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
                    parameter.Value = storage_ID;
                    command.Parameters.Add(parameter);

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

            return products;
        }
    }
}