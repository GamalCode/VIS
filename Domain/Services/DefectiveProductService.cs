using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class DefectiveProductService
    {
        public List<DefectiveProduct> GetAllDefectiveProducts()
        {
            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            var daoDefectiveProducts = defectiveProductDAO.GetAll();
            return DefectiveProductMapper.FromDAOList(daoDefectiveProducts);
        }

        public DefectiveProduct GetDefectiveProductById(int id)
        {
            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            var daoDefectiveProduct = defectiveProductDAO.GetById(id);
            return DefectiveProductMapper.FromDAO(daoDefectiveProduct);
        }

        public List<DefectiveProduct> GetDefectiveProductsByStorageId(int storageId)
        {
            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            var daoDefectiveProducts = defectiveProductDAO.GetByStorage_ID(storageId);
            return DefectiveProductMapper.FromDAOList(daoDefectiveProducts);
        }

        public List<DefectiveProduct> GetDefectiveProductsByProductId(int productId)
        {
            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            var daoDefectiveProducts = defectiveProductDAO.GetByProduct_ID(productId);
            return DefectiveProductMapper.FromDAOList(daoDefectiveProducts);
        }

        public int CreateDefectiveProduct(DefectiveProduct defectiveProduct)
        {
            ValidateDefectiveProduct(defectiveProduct);

            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            var daoDefectiveProduct = DefectiveProductMapper.ToDAO(defectiveProduct);
            return defectiveProductDAO.Insert(daoDefectiveProduct);
        }

        public void UpdateDefectiveProduct(DefectiveProduct defectiveProduct)
        {
            ValidateDefectiveProduct(defectiveProduct);

            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            var daoDefectiveProduct = DefectiveProductMapper.ToDAO(defectiveProduct);
            defectiveProductDAO.Update(daoDefectiveProduct);
        }

        public void DeleteDefectiveProduct(int defectiveProductId)
        {
            var defectiveProductDAO = GlobalConfig.Connection.GetDefectiveProductDAO();
            defectiveProductDAO.Delete(defectiveProductId);
        }

        public int GetTotalDefectiveQuantity(int productId)
        {
            var defectiveProducts = GetDefectiveProductsByProductId(productId);
            int total = 0;

            foreach (var defective in defectiveProducts)
            {
                total += defective.Quantity;
            }

            return total;
        }

        public int GetTotalDefectiveInStorage(int storageId)
        {
            var defectiveProducts = GetDefectiveProductsByStorageId(storageId);
            int total = 0;

            foreach (var defective in defectiveProducts)
            {
                total += defective.Quantity;
            }

            return total;
        }

        public string GetDefectiveProductInfo(int defectiveProductId)
        {
            var defectiveProduct = GetDefectiveProductById(defectiveProductId);
            if (defectiveProduct == null) return "Neznámý poškozený produkt";

            var productService = new ProductService();
            var storageService = new StorageService();

            var product = productService.GetProductById(defectiveProduct.Product_ID);
            var storage = storageService.GetStorageById(defectiveProduct.Storage_ID);

            var productName = product?.Name ?? "Neznámý produkt";
            var storageName = storage?.Storage_Location ?? "Neznámý sklad";

            return $"{productName} - {defectiveProduct.Quantity} ks - {storageName} - {defectiveProduct.Reason}";
        }

        public double GetDefectivePercentage(int productId)
        {
            var stockService = new StockService();
            int healthyStock = stockService.GetAvailableQuantity(productId);
            int defectiveStock = GetTotalDefectiveQuantity(productId);
            int total = healthyStock + defectiveStock;

            if (total == 0) return 0;

            return Math.Round((double)defectiveStock / total * 100, 2);
        }

        private void ValidateDefectiveProduct(DefectiveProduct defectiveProduct)
        {
            if (defectiveProduct == null)
                throw new ArgumentNullException(nameof(defectiveProduct));

            if (defectiveProduct.Product_ID <= 0)
                throw new ArgumentException("Produkt musí být vybrán.");

            if (defectiveProduct.Storage_ID <= 0)
                throw new ArgumentException("Sklad musí být vybrán.");

            if (defectiveProduct.Quantity <= 0)
                throw new ArgumentException("Množství musí být větší než 0.");

            if (defectiveProduct.Report_Date == default)
                throw new ArgumentException("Datum hlášení musí být nastaveno.");
        }
    }
}