using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            var productDAO = GlobalConfig.Connection.GetProductDAO();
            var daoProducts = productDAO.GetAll();
            return ProductMapper.FromDAOList(daoProducts);
        }

        public Product GetProductById(int id)
        {
            var productDAO = GlobalConfig.Connection.GetProductDAO();
            var daoProduct = productDAO.GetById(id);
            return ProductMapper.FromDAO(daoProduct);
        }

        public List<Product> GetProductsBySupplier(int supplierId)
        {
            var productDAO = GlobalConfig.Connection.GetProductDAO();
            var daoProducts = productDAO.GetBySupplier_ID(supplierId);
            return ProductMapper.FromDAOList(daoProducts);
        }

        public List<Product> GetProductsByStorage(int storageId)
        {
            var productDAO = GlobalConfig.Connection.GetProductDAO();
            var daoProducts = productDAO.GetByStorage_ID(storageId);
            return ProductMapper.FromDAOList(daoProducts);
        }

        public int CreateProduct(Product product)
        {
            ValidateProduct(product);

            var productDAO = GlobalConfig.Connection.GetProductDAO();
            var daoProduct = ProductMapper.ToDAO(product);
            return productDAO.Insert(daoProduct);
        }

        public void UpdateProduct(Product product)
        {
            ValidateProduct(product);

            var productDAO = GlobalConfig.Connection.GetProductDAO();
            var daoProduct = ProductMapper.ToDAO(product);
            productDAO.Update(daoProduct);
        }

        public void DeleteProduct(int productId)
        {
            var productDAO = GlobalConfig.Connection.GetProductDAO();
            productDAO.Delete(productId);
        }

        public int GetTotalStock(int productId)
        {
            var stockService = new StockService();
            var stocks = stockService.GetStocksByProductId(productId);

            int total = 0;
            foreach (var stock in stocks)
            {
                total += stock.Quantity;
            }

            return total;
        }

        public string GetFormattedProductInfo(int productId)
        {
            var product = GetProductById(productId);
            if (product == null) return "Neznámý produkt";

            var supplierService = new SupplierService();
            var supplier = supplierService.GetSupplierById(product.Supplier_ID);
            var supplierName = supplier?.Name ?? "Neznámý dodavatel";

            return $"{product.Name} ({product.Type}) - {product.CarModel} - {supplierName}";
        }

        private void ValidateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Název produktu nemůže být prázdný.");

            if (product.Price < 0)
                throw new ArgumentException("Cena nemůže být záporná.");

            if (product.Supplier_ID <= 0)
                throw new ArgumentException("Dodavatel musí být vybrán.");

            if (product.Storage_ID <= 0)
                throw new ArgumentException("Sklad musí být vybrán.");
        }
    }
}