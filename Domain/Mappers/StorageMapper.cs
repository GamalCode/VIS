namespace Domain.Mappers
{
    public static class StorageMapper
    {
        public static Models.Storage FromDAO(DataAccess.DAO.Storage daoStorage)
        {
            if (daoStorage == null) return null;

            return new Models.Storage
            {
                Storage_ID = daoStorage.Storage_ID,
                Storage_Location = daoStorage.Storage_Location,
                Storage_Capacity = daoStorage.Storage_Capacity,
                Last_Updated = daoStorage.Last_Updated
            };
        }

        public static DataAccess.DAO.Storage ToDAO(Models.Storage domainStorage)
        {
            if (domainStorage == null) return null;

            return new DataAccess.DAO.Storage
            {
                Storage_ID = domainStorage.Storage_ID,
                Storage_Location = domainStorage.Storage_Location,
                Storage_Capacity = domainStorage.Storage_Capacity,
                Last_Updated = domainStorage.Last_Updated
            };
        }

        public static List<Models.Storage> FromDAOList(List<DataAccess.DAO.Storage> daoStorages)
        {
            return daoStorages?.Select(FromDAO).ToList() ?? new List<Models.Storage>();
        }

        public static List<DataAccess.DAO.Storage> ToDAOList(List<Models.Storage> domainStorages)
        {
            return domainStorages?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.Storage>();
        }
    }
}