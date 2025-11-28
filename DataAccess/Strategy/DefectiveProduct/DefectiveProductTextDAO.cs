using System.Text.Json;

namespace DataAccess.Strategy.DefectiveProduct
{
    public class DefectiveProductTextDAO : IDefectiveProductDAO
    {
        private readonly string _filePath;

        public DefectiveProductTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "defectiveproducts.json");
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

        private List<DAO.DefectiveProduct> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.DefectiveProduct>>(json) ?? new List<DAO.DefectiveProduct>();
        }

        private void WriteAll(List<DAO.DefectiveProduct> defectiveProducts)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(defectiveProducts, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.DefectiveProduct> GetAll()
        {
            return ReadAll();
        }

        public DAO.DefectiveProduct GetById(int id)
        {
            var defectiveProducts = ReadAll();
            return defectiveProducts.FirstOrDefault(d => d.Defective_ID == id);
        }

        public int Insert(DAO.DefectiveProduct defectiveProduct)
        {
            var defectiveProducts = ReadAll();
            defectiveProduct.Defective_ID = defectiveProducts.Any() ? defectiveProducts.Max(d => d.Defective_ID) + 1 : 1;
            defectiveProducts.Add(defectiveProduct);
            WriteAll(defectiveProducts);
            return defectiveProduct.Defective_ID;
        }

        public void Update(DAO.DefectiveProduct defectiveProduct)
        {
            var defectiveProducts = ReadAll();
            var index = defectiveProducts.FindIndex(d => d.Defective_ID == defectiveProduct.Defective_ID);
            if (index >= 0)
            {
                defectiveProducts[index] = defectiveProduct;
                WriteAll(defectiveProducts);
            }
        }

        public void Delete(int id)
        {
            var defectiveProducts = ReadAll();
            defectiveProducts.RemoveAll(d => d.Defective_ID == id);
            WriteAll(defectiveProducts);
        }

        public List<DAO.DefectiveProduct> GetByStorage_ID(int storage_ID)
        {
            var defectiveProducts = ReadAll();
            return defectiveProducts.Where(d => d.Storage_ID == storage_ID).ToList();
        }

        public List<DAO.DefectiveProduct> GetByProduct_ID(int product_ID)
        {
            var defectiveProducts = ReadAll();
            return defectiveProducts.Where(d => d.Product_ID == product_ID).ToList();
        }
    }
}