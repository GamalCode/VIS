using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class StockService
    {
        public List<Stock> GetAllStocks()
        {
            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            var daoStocks = stockDAO.GetAll();
            return StockMapper.FromDAOList(daoStocks);
        }

        public Stock GetStockById(int id)
        {
            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            var daoStock = stockDAO.GetById(id);
            return StockMapper.FromDAO(daoStock);
        }

        public List<Stock> GetStocksByProductId(int productId)
        {
            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            var daoStocks = stockDAO.GetByProduct_ID(productId);
            return StockMapper.FromDAOList(daoStocks);
        }

        public List<Stock> GetStocksByStorageId(int storageId)
        {
            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            var daoStocks = stockDAO.GetByStorage_ID(storageId);
            return StockMapper.FromDAOList(daoStocks);
        }

        public int CreateStock(Stock stock)
        {
            ValidateStock(stock);

            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            var daoStock = StockMapper.ToDAO(stock);
            return stockDAO.Insert(daoStock);
        }

        public void UpdateStock(Stock stock)
        {
            ValidateStock(stock);

            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            var daoStock = StockMapper.ToDAO(stock);
            stockDAO.Update(daoStock);
        }

        public void DeleteStock(int stockId)
        {
            var stockDAO = GlobalConfig.Connection.GetStockDAO();
            stockDAO.Delete(stockId);
        }

        public bool HasSufficientStock(int productId, int quantity)
        {
            var stocks = GetStocksByProductId(productId);
            int totalAvailable = 0;

            foreach (var stock in stocks)
            {
                totalAvailable += stock.Quantity;
            }

            return totalAvailable >= quantity;
        }

        public int GetAvailableQuantity(int productId)
        {
            var stocks = GetStocksByProductId(productId);
            int total = 0;

            foreach (var stock in stocks)
            {
                total += stock.Quantity;
            }

            return total;
        }

        public string GetStockInfo(int stockId)
        {
            var stock = GetStockById(stockId);
            if (stock == null) return "Neznámá zásoba";

            var productService = new ProductService();
            var storageService = new StorageService();

            var product = productService.GetProductById(stock.Product_ID);
            var storage = storageService.GetStorageById(stock.Storage_ID);

            var productName = product?.Name ?? "Neznámý produkt";
            var storageName = storage?.Storage_Location ?? "Neznámý sklad";

            return $"{productName} - {stock.Quantity} ks - {storageName}";
        }

        private void ValidateStock(Stock stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            if (stock.Product_ID <= 0)
                throw new ArgumentException("Produkt musí být vybrán.");

            if (stock.Storage_ID <= 0)
                throw new ArgumentException("Sklad musí být vybrán.");

            if (stock.Quantity < 0)
                throw new ArgumentException("Množství nemůže být záporné.");
        }
    }
}