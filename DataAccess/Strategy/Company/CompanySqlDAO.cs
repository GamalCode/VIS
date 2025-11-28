using DataAccess.Database;

namespace DataAccess.Strategy.Company
{
    public class CompanySqlDAO : ICompanyDAO
    {
        private readonly DatabaseConnection _dbConnection;

        public CompanySqlDAO(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<DAO.Company> GetAll()
        {
            var companies = new List<DAO.Company>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Company_ID, Company_Name, Contact_Email, Contact_Phone, Storage_ID FROM Company";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            companies.Add(new DAO.Company
                            {
                                Company_ID = reader.GetInt32(0),
                                Company_Name = reader.GetString(1),
                                Contact_Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Contact_Phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return companies;
        }

        public DAO.Company GetById(int id)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Company_ID, Company_Name, Contact_Email, Contact_Phone, Storage_ID FROM Company WHERE Company_ID = @Company_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Company_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Company
                            {
                                Company_ID = reader.GetInt32(0),
                                Company_Name = reader.GetString(1),
                                Contact_Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Contact_Phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public int Insert(DAO.Company company)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Company (Company_Name, Contact_Email, Contact_Phone, Storage_ID) 
                                          VALUES (@Company_Name, @Contact_Email, @Contact_Phone, @Storage_ID);
                                          SELECT last_insert_rowid();";

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@Company_Name";
                    paramName.Value = company.Company_Name;
                    command.Parameters.Add(paramName);

                    var paramEmail = command.CreateParameter();
                    paramEmail.ParameterName = "@Contact_Email";
                    paramEmail.Value = string.IsNullOrEmpty(company.Contact_Email) ? DBNull.Value : company.Contact_Email;
                    command.Parameters.Add(paramEmail);

                    var paramPhone = command.CreateParameter();
                    paramPhone.ParameterName = "@Contact_Phone";
                    paramPhone.Value = string.IsNullOrEmpty(company.Contact_Phone) ? DBNull.Value : company.Contact_Phone;
                    command.Parameters.Add(paramPhone);

                    var paramStorageID = command.CreateParameter();
                    paramStorageID.ParameterName = "@Storage_ID";
                    paramStorageID.Value = company.Storage_ID;
                    command.Parameters.Add(paramStorageID);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    company.Company_ID = id;
                    return id;
                }
            }
        }

        public void Update(DAO.Company company)
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE Company 
                                          SET Company_Name = @Company_Name, Contact_Email = @Contact_Email, Contact_Phone = @Contact_Phone, Storage_ID = @Storage_ID 
                                          WHERE Company_ID = @Company_ID";

                    var paramCompany_ID = command.CreateParameter();
                    paramCompany_ID.ParameterName = "@Company_ID";
                    paramCompany_ID.Value = company.Company_ID;
                    command.Parameters.Add(paramCompany_ID);

                    var paramName = command.CreateParameter();
                    paramName.ParameterName = "@Company_Name";
                    paramName.Value = company.Company_Name;
                    command.Parameters.Add(paramName);

                    var paramEmail = command.CreateParameter();
                    paramEmail.ParameterName = "@Contact_Email";
                    paramEmail.Value = string.IsNullOrEmpty(company.Contact_Email) ? DBNull.Value : company.Contact_Email;
                    command.Parameters.Add(paramEmail);

                    var paramPhone = command.CreateParameter();
                    paramPhone.ParameterName = "@Contact_Phone";
                    paramPhone.Value = string.IsNullOrEmpty(company.Contact_Phone) ? DBNull.Value : company.Contact_Phone;
                    command.Parameters.Add(paramPhone);

                    var paramStorageID = command.CreateParameter();
                    paramStorageID.ParameterName = "@Storage_ID";
                    paramStorageID.Value = company.Storage_ID;
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
                    command.CommandText = "DELETE FROM Company WHERE Company_ID = @Company_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Company_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<DAO.Company> GetByName(string name)
        {
            var companies = new List<DAO.Company>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Company_ID, Company_Name, Contact_Email, Contact_Phone, Storage_ID FROM Company WHERE Company_Name LIKE @Company_Name";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Company_Name";
                    parameter.Value = "%" + name + "%";
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            companies.Add(new DAO.Company
                            {
                                Company_ID = reader.GetInt32(0),
                                Company_Name = reader.GetString(1),
                                Contact_Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Contact_Phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return companies;
        }

        public List<DAO.Company> GetByStorage_ID(int storage_ID)
        {
            var companies = new List<DAO.Company>();

            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Company_ID, Company_Name, Contact_Email, Contact_Phone, Storage_ID FROM Company WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
                    parameter.Value = storage_ID;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            companies.Add(new DAO.Company
                            {
                                Company_ID = reader.GetInt32(0),
                                Company_Name = reader.GetString(1),
                                Contact_Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Contact_Phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Storage_ID = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return companies;
        }
    }
}