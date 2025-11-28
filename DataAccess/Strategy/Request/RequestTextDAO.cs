using System.Text.Json;

namespace DataAccess.Strategy.Request
{
    public class RequestTextDAO : IRequestDAO
    {
        private readonly string _filePath;

        public RequestTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "requests.json");
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

        private List<DAO.Request> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.Request>>(json) ?? new List<DAO.Request>();
        }

        private void WriteAll(List<DAO.Request> requests)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(requests, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.Request> GetAll()
        {
            return ReadAll();
        }

        public DAO.Request GetById(int id)
        {
            var requests = ReadAll();
            return requests.FirstOrDefault(r => r.Request_ID == id);
        }

        public int Insert(DAO.Request request)
        {
            var requests = ReadAll();
            request.Request_ID = requests.Any() ? requests.Max(r => r.Request_ID) + 1 : 1;
            requests.Add(request);
            WriteAll(requests);
            return request.Request_ID;
        }

        public void Update(DAO.Request request)
        {
            var requests = ReadAll();
            var index = requests.FindIndex(r => r.Request_ID == request.Request_ID);
            if (index >= 0)
            {
                requests[index] = request;
                WriteAll(requests);
            }
        }

        public void Delete(int id)
        {
            var requests = ReadAll();
            requests.RemoveAll(r => r.Request_ID == id);
            WriteAll(requests);
        }

        public List<DAO.Request> GetByCompany_ID(int company_ID)
        {
            var requests = ReadAll();
            return requests.Where(r => r.Company_ID == company_ID).ToList();
        }

        public List<DAO.Request> GetByStatus(string status)
        {
            var requests = ReadAll();
            return requests.Where(r => r.Status == status).ToList();
        }
    }
}