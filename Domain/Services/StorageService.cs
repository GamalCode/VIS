using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class StorageService
    {
        public List<Storage> GetAllStorages()
        {
            var storageDAO = GlobalConfig.Connection.GetStorageDAO();
            var daoStorages = storageDAO.GetAll();
            return StorageMapper.FromDAOList(daoStorages);
        }

        public Storage GetStorageById(int id)
        {
            var storageDAO = GlobalConfig.Connection.GetStorageDAO();
            var daoStorage = storageDAO.GetById(id);
            return StorageMapper.FromDAO(daoStorage);
        }

        public List<Storage> GetStoragesByLocation(string location)
        {
            var storageDAO = GlobalConfig.Connection.GetStorageDAO();
            var daoStorages = storageDAO.GetByLocation(location);
            return StorageMapper.FromDAOList(daoStorages);
        }

        public int CreateStorage(Storage storage)
        {
            ValidateStorage(storage);

            var storageDAO = GlobalConfig.Connection.GetStorageDAO();
            var daoStorage = StorageMapper.ToDAO(storage);
            return storageDAO.Insert(daoStorage);
        }

        public void UpdateStorage(Storage storage)
        {
            ValidateStorage(storage);

            var storageDAO = GlobalConfig.Connection.GetStorageDAO();
            var daoStorage = StorageMapper.ToDAO(storage);
            storageDAO.Update(daoStorage);
        }

        public void DeleteStorage(int storageId)
        {
            var storageDAO = GlobalConfig.Connection.GetStorageDAO();
            storageDAO.Delete(storageId);
        }

        public int GetAvailableCapacity(int storageId)
        {
            var storage = GetStorageById(storageId);
            if (storage == null) return 0;

            var stockService = new StockService();
            var stocks = stockService.GetStocksByStorageId(storageId);

            int usedCapacity = 0;
            foreach (var stock in stocks)
            {
                usedCapacity += stock.Quantity;
            }

            return storage.Storage_Capacity - usedCapacity;
        }

        private void ValidateStorage(Storage storage)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            if (string.IsNullOrWhiteSpace(storage.Storage_Location))
                throw new ArgumentException("Umístění skladu nemůže být prázdné.");

            if (storage.Storage_Capacity < 0)
                throw new ArgumentException("Kapacita skladu nemůže být záporná.");
        }
    }
}