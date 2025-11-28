using DataAccess.Database;

namespace DataAccess.Strategy.DefectiveProduct
{
    public class DefectiveProductSqlDAO : IDefectiveProductDAO
    {
        private readonly DatabaseConnection _dbConnection;

        public DefectiveProductSqlDAO(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<DAO.DefectiveProduct> GetAll()
        {
            var defectiveProducts = new List<DAO.DefectiveProduct>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            defectiveProducts.Add(new DAO.DefectiveProduct
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

            return defectiveProducts;
        }

        public DAO.DefectiveProduct GetById(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct WHERE Defective_ID = @Defective_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Defective_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

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

            return null;
        }

        public int Insert(DAO.DefectiveProduct defectiveProduct)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO DefectiveProduct (Product_ID, Storage_ID, Quantity, Report_Date, Reason) 
                                          VALUES (@Product_ID, @Storage_ID, @Quantity, @Report_Date, @Reason);
                                          SELECT last_insert_rowid();";

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = defectiveProduct.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramStorage_ID = command.CreateParameter();
                    paramStorage_ID.ParameterName = "@Storage_ID";
                    paramStorage_ID.Value = defectiveProduct.Storage_ID;
                    command.Parameters.Add(paramStorage_ID);

                    var paramQuantity = command.CreateParameter();
                    paramQuantity.ParameterName = "@Quantity";
                    paramQuantity.Value = defectiveProduct.Quantity;
                    command.Parameters.Add(paramQuantity);

                    var paramReport_Date = command.CreateParameter();
                    paramReport_Date.ParameterName = "@Report_Date";
                    paramReport_Date.Value = defectiveProduct.Report_Date.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.Add(paramReport_Date);

                    var paramReason = command.CreateParameter();
                    paramReason.ParameterName = "@Reason";
                    paramReason.Value = string.IsNullOrEmpty(defectiveProduct.Reason) ? DBNull.Value : defectiveProduct.Reason;
                    command.Parameters.Add(paramReason);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    defectiveProduct.Defective_ID = id;
                    return id;
                }
            }
        }

        public void Update(DAO.DefectiveProduct defectiveProduct)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE DefectiveProduct 
                                          SET Product_ID = @Product_ID, Storage_ID = @Storage_ID, Quantity = @Quantity, Report_Date = @Report_Date, Reason = @Reason 
                                          WHERE Defective_ID = @Defective_ID";

                    var paramDefective_ID = command.CreateParameter();
                    paramDefective_ID.ParameterName = "@Defective_ID";
                    paramDefective_ID.Value = defectiveProduct.Defective_ID;
                    command.Parameters.Add(paramDefective_ID);

                    var paramProduct_ID = command.CreateParameter();
                    paramProduct_ID.ParameterName = "@Product_ID";
                    paramProduct_ID.Value = defectiveProduct.Product_ID;
                    command.Parameters.Add(paramProduct_ID);

                    var paramStorage_ID = command.CreateParameter();
                    paramStorage_ID.ParameterName = "@Storage_ID";
                    paramStorage_ID.Value = defectiveProduct.Storage_ID;
                    command.Parameters.Add(paramStorage_ID);

                    var paramQuantity = command.CreateParameter();
                    paramQuantity.ParameterName = "@Quantity";
                    paramQuantity.Value = defectiveProduct.Quantity;
                    command.Parameters.Add(paramQuantity);

                    var paramReport_Date = command.CreateParameter();
                    paramReport_Date.ParameterName = "@Report_Date";
                    paramReport_Date.Value = defectiveProduct.Report_Date.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.Add(paramReport_Date);

                    var paramReason = command.CreateParameter();
                    paramReason.ParameterName = "@Reason";
                    paramReason.Value = string.IsNullOrEmpty(defectiveProduct.Reason) ? DBNull.Value : defectiveProduct.Reason;
                    command.Parameters.Add(paramReason);

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
                    command.CommandText = "DELETE FROM DefectiveProduct WHERE Defective_ID = @Defective_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Defective_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<DAO.DefectiveProduct> GetByStorage_ID(int storage_ID)
        {
            var defectiveProducts = new List<DAO.DefectiveProduct>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
                    parameter.Value = storage_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            defectiveProducts.Add(new DAO.DefectiveProduct
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

            return defectiveProducts;
        }

        public List<DAO.DefectiveProduct> GetByProduct_ID(int product_ID)
        {
            var defectiveProducts = new List<DAO.DefectiveProduct>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Defective_ID, Product_ID, Storage_ID, Quantity, Report_Date, Reason FROM DefectiveProduct WHERE Product_ID = @Product_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Product_ID";
                    parameter.Value = product_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            defectiveProducts.Add(new DAO.DefectiveProduct
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

            return defectiveProducts;
        }
    }
}