using System.Text.Json;

namespace DataAccess.Strategy.Stock
{
    public class StockTextDAO : IStockDAO
    {
        private readonly string _filePath;

        public StockTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "stocks.json");
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

        private List<DAO.Stock> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.Stock>>(json) ?? new List<DAO.Stock>();
        }

        private void WriteAll(List<DAO.Stock> stocks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(stocks, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.Stock> GetAll()
        {
            return ReadAll();
        }

        public DAO.Stock GetById(int id)
        {
            var stocks = ReadAll();
            return stocks.FirstOrDefault(s => s.Stock_ID == id);
        }

        public int Insert(DAO.Stock stock)
        {
            var stocks = ReadAll();
            stock.Stock_ID = stocks.Any() ? stocks.Max(s => s.Stock_ID) + 1 : 1;
            stocks.Add(stock);
            WriteAll(stocks);
            return stock.Stock_ID;
        }

        public void Update(DAO.Stock stock)
        {
            var stocks = ReadAll();
            var index = stocks.FindIndex(s => s.Stock_ID == stock.Stock_ID);
            if (index >= 0)
            {
                stocks[index] = stock;
                WriteAll(stocks);
            }
        }

        public void Delete(int id)
        {
            var stocks = ReadAll();
            stocks.RemoveAll(s => s.Stock_ID == id);
            WriteAll(stocks);
        }

        public List<DAO.Stock> GetByProduct_ID(int product_ID)
        {
            var stocks = ReadAll();
            return stocks.Where(s => s.Product_ID == product_ID).ToList();
        }

        public List<DAO.Stock> GetByStorage_ID(int storage_ID)
        {
            var stocks = ReadAll();
            return stocks.Where(s => s.Storage_ID == storage_ID).ToList();
        }
    }
}