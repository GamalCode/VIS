using System.Text.Json;

namespace DataAccess.Strategy.Storage
{
    public class StorageTextDAO : IStorageDAO
    {
        private readonly string _filePath;

        public StorageTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "storages.json");
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private List<DAO.Storage> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.Storage>>(json) ?? new List<DAO.Storage>();
        }

        private void WriteAll(List<DAO.Storage> storages)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(storages, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.Storage> GetAll()
        {
            return ReadAll();
        }

        public DAO.Storage GetById(int id)
        {
            var storages = ReadAll();
            return storages.FirstOrDefault(s => s.Storage_ID == id);
        }

        public int Insert(DAO.Storage storage)
        {
            var storages = ReadAll();
            storage.Storage_ID = storages.Any() ? storages.Max(s => s.Storage_ID) + 1 : 1;
            storages.Add(storage);
            WriteAll(storages);
            return storage.Storage_ID;
        }

        public void Update(DAO.Storage storage)
        {
            var storages = ReadAll();
            var index = storages.FindIndex(s => s.Storage_ID == storage.Storage_ID);
            if (index >= 0)
            {
                storages[index] = storage;
                WriteAll(storages);
            }
        }

        public void Delete(int id)
        {
            var storages = ReadAll();
            storages.RemoveAll(s => s.Storage_ID == id);
            WriteAll(storages);
        }

        public List<DAO.Storage> GetByLocation(string location)
        {
            var storages = ReadAll();
            return storages.Where(s => s.Storage_Location.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}