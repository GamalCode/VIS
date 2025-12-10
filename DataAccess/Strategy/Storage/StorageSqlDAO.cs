using DataAccess.Database;
using DataAccess.UnitOfWork;
using System.Data;

namespace DataAccess.Strategy.Storage
{
    public class StorageSqlDAO : IStorageDAO
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly IUnitOfWork? _unitOfWork;

        public StorageSqlDAO(DatabaseConnection dbConnection, IUnitOfWork? unitOfWork = null)
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

        public List<DAO.Storage> GetAll()
        {
            var storages = new List<DAO.Storage>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Storage_ID, Storage_Location, Storage_Capacity, Last_Updated FROM Storage";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storages.Add(new DAO.Storage
                            {
                                Storage_ID = reader.GetInt32(0),
                                Storage_Location = reader.GetString(1),
                                Storage_Capacity = reader.GetInt32(2),
                                Last_Updated = DateTime.Parse(reader.GetString(3))
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

            return storages;
        }

        public DAO.Storage GetById(int id)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Storage_ID, Storage_Location, Storage_Capacity, Last_Updated FROM Storage WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DAO.Storage
                            {
                                Storage_ID = reader.GetInt32(0),
                                Storage_Location = reader.GetString(1),
                                Storage_Capacity = reader.GetInt32(2),
                                Last_Updated = DateTime.Parse(reader.GetString(3))
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

        public int Insert(DAO.Storage storage)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"INSERT INTO Storage (Storage_Location, Storage_Capacity, Last_Updated) 
                                          VALUES (@Storage_Location, @Storage_Capacity, @Last_Updated);
                                          SELECT last_insert_rowid();";

                    var paramLocation = command.CreateParameter();
                    paramLocation.ParameterName = "@Storage_Location";
                    paramLocation.Value = storage.Storage_Location;
                    command.Parameters.Add(paramLocation);

                    var paramCapacity = command.CreateParameter();
                    paramCapacity.ParameterName = "@Storage_Capacity";
                    paramCapacity.Value = storage.Storage_Capacity;
                    command.Parameters.Add(paramCapacity);

                    var paramLastUpdated = command.CreateParameter();
                    paramLastUpdated.ParameterName = "@Last_Updated";
                    paramLastUpdated.Value = storage.Last_Updated.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.Add(paramLastUpdated);

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    storage.Storage_ID = id;
                    return id;
                }
            }
            finally
            {
                if (ShouldDisposeConnection)
                    connection.Dispose();
            }
        }

        public void Update(DAO.Storage storage)
        {
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = @"UPDATE Storage 
                                          SET Storage_Location = @Storage_Location, Storage_Capacity = @Storage_Capacity, Last_Updated = @Last_Updated 
                                          WHERE Storage_ID = @Storage_ID";

                    var paramStorage_ID = command.CreateParameter();
                    paramStorage_ID.ParameterName = "@Storage_ID";
                    paramStorage_ID.Value = storage.Storage_ID;
                    command.Parameters.Add(paramStorage_ID);

                    var paramLocation = command.CreateParameter();
                    paramLocation.ParameterName = "@Storage_Location";
                    paramLocation.Value = storage.Storage_Location;
                    command.Parameters.Add(paramLocation);

                    var paramCapacity = command.CreateParameter();
                    paramCapacity.ParameterName = "@Storage_Capacity";
                    paramCapacity.Value = storage.Storage_Capacity;
                    command.Parameters.Add(paramCapacity);

                    var paramLastUpdated = command.CreateParameter();
                    paramLastUpdated.ParameterName = "@Last_Updated";
                    paramLastUpdated.Value = storage.Last_Updated.ToString("yyyy-MM-dd HH:mm:ss");
                    command.Parameters.Add(paramLastUpdated);

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
                    command.CommandText = "DELETE FROM Storage WHERE Storage_ID = @Storage_ID";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_ID";
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

        public List<DAO.Storage> GetByLocation(string location)
        {
            var storages = new List<DAO.Storage>();
            var connection = GetConnection();

            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = _unitOfWork?.Transaction;
                    command.CommandText = "SELECT Storage_ID, Storage_Location, Storage_Capacity, Last_Updated FROM Storage WHERE Storage_Location LIKE @Storage_Location";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@Storage_Location";
                    parameter.Value = "%" + location + "%";
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storages.Add(new DAO.Storage
                            {
                                Storage_ID = reader.GetInt32(0),
                                Storage_Location = reader.GetString(1),
                                Storage_Capacity = reader.GetInt32(2),
                                Last_Updated = DateTime.Parse(reader.GetString(3))
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

            return storages;
        }
    }
}