using System.Text.Json;

namespace DataAccess.Strategy.Supplier
{
    public class SupplierTextDAO : ISupplierDAO
    {
        private readonly string _filePath;

        public SupplierTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "suppliers.json");
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

        private List<DAO.Supplier> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.Supplier>>(json) ?? new List<DAO.Supplier>();
        }

        private void WriteAll(List<DAO.Supplier> suppliers)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(suppliers, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.Supplier> GetAll()
        {
            return ReadAll();
        }

        public DAO.Supplier GetById(int id)
        {
            var suppliers = ReadAll();
            return suppliers.FirstOrDefault(s => s.Supplier_ID == id);
        }

        public int Insert(DAO.Supplier supplier)
        {
            var suppliers = ReadAll();
            supplier.Supplier_ID = suppliers.Any() ? suppliers.Max(s => s.Supplier_ID) + 1 : 1;
            suppliers.Add(supplier);
            WriteAll(suppliers);
            return supplier.Supplier_ID;
        }

        public void Update(DAO.Supplier supplier)
        {
            var suppliers = ReadAll();
            var index = suppliers.FindIndex(s => s.Supplier_ID == supplier.Supplier_ID);
            if (index >= 0)
            {
                suppliers[index] = supplier;
                WriteAll(suppliers);
            }
        }

        public void Delete(int id)
        {
            var suppliers = ReadAll();
            suppliers.RemoveAll(s => s.Supplier_ID == id);
            WriteAll(suppliers);
        }

        public List<DAO.Supplier> GetByName(string name)
        {
            var suppliers = ReadAll();
            return suppliers.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<DAO.Supplier> GetByStorage_ID(int storage_ID)
        {
            var suppliers = ReadAll();
            return suppliers.Where(s => s.Storage_ID == storage_ID).ToList();
        }
    }
}