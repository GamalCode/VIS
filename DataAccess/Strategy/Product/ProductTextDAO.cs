using System.Text.Json;

namespace DataAccess.Strategy.Product
{
    public class ProductTextDAO : IProductDAO
    {
        private readonly string _filePath;

        public ProductTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "products.json");
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

        private List<DAO.Product> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.Product>>(json) ?? new List<DAO.Product>();
        }

        private void WriteAll(List<DAO.Product> products)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(products, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.Product> GetAll()
        {
            return ReadAll();
        }

        public DAO.Product GetById(int id)
        {
            var products = ReadAll();
            return products.FirstOrDefault(p => p.Product_ID == id);
        }

        public int Insert(DAO.Product product)
        {
            var products = ReadAll();
            product.Product_ID = products.Any() ? products.Max(p => p.Product_ID) + 1 : 1;
            products.Add(product);
            WriteAll(products);
            return product.Product_ID;
        }

        public void Update(DAO.Product product)
        {
            var products = ReadAll();
            var index = products.FindIndex(p => p.Product_ID == product.Product_ID);
            if (index >= 0)
            {
                products[index] = product;
                WriteAll(products);
            }
        }

        public void Delete(int id)
        {
            var products = ReadAll();
            products.RemoveAll(p => p.Product_ID == id);
            WriteAll(products);
        }

        public List<DAO.Product> GetBySupplier_ID(int supplier_ID)
        {
            var products = ReadAll();
            return products.Where(p => p.Supplier_ID == supplier_ID).ToList();
        }

        public List<DAO.Product> GetByStorage_ID(int storage_ID)
        {
            var products = ReadAll();
            return products.Where(p => p.Storage_ID == storage_ID).ToList();
        }
    }
}