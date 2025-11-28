using System.Text.Json;

namespace DataAccess.Strategy.RequestStock
{
    public class RequestStockTextDAO : IRequestStockDAO
    {
        private readonly string _filePath;

        public RequestStockTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "requeststocks.json");
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

        private List<DAO.RequestStock> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.RequestStock>>(json) ?? new List<DAO.RequestStock>();
        }

        private void WriteAll(List<DAO.RequestStock> requestStocks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(requestStocks, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.RequestStock> GetAll()
        {
            return ReadAll();
        }

        public DAO.RequestStock GetById(int id)
        {
            var requestStocks = ReadAll();
            return requestStocks.FirstOrDefault(rs => rs.Request_Stock_ID == id);
        }

        public int Insert(DAO.RequestStock requestStock)
        {
            var requestStocks = ReadAll();
            requestStock.Request_Stock_ID = requestStocks.Any() ? requestStocks.Max(rs => rs.Request_Stock_ID) + 1 : 1;
            requestStocks.Add(requestStock);
            WriteAll(requestStocks);
            return requestStock.Request_Stock_ID;
        }

        public void Update(DAO.RequestStock requestStock)
        {
            var requestStocks = ReadAll();
            var index = requestStocks.FindIndex(rs => rs.Request_Stock_ID == requestStock.Request_Stock_ID);
            if (index >= 0)
            {
                requestStocks[index] = requestStock;
                WriteAll(requestStocks);
            }
        }

        public void Delete(int id)
        {
            var requestStocks = ReadAll();
            requestStocks.RemoveAll(rs => rs.Request_Stock_ID == id);
            WriteAll(requestStocks);
        }

        public List<DAO.RequestStock> GetByRequest_ID(int request_ID)
        {
            var requestStocks = ReadAll();
            return requestStocks.Where(rs => rs.Request_ID == request_ID).ToList();
        }

        public List<DAO.RequestStock> GetByStock_ID(int stock_ID)
        {
            var requestStocks = ReadAll();
            return requestStocks.Where(rs => rs.Stock_ID == stock_ID).ToList();
        }
    }
}