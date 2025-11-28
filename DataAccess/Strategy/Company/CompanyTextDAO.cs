using System.Text.Json;

namespace DataAccess.Strategy.Company
{
    public class CompanyTextDAO : ICompanyDAO
    {
        private readonly string _filePath;

        public CompanyTextDAO(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "companies.json");
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

        private List<DAO.Company> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<DAO.Company>>(json) ?? new List<DAO.Company>();
        }

        private void WriteAll(List<DAO.Company> companies)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(companies, options);
            File.WriteAllText(_filePath, json);
        }

        public List<DAO.Company> GetAll()
        {
            return ReadAll();
        }

        public DAO.Company GetById(int id)
        {
            var companies = ReadAll();
            return companies.FirstOrDefault(c => c.Company_ID == id);
        }

        public int Insert(DAO.Company company)
        {
            var companies = ReadAll();
            company.Company_ID = companies.Any() ? companies.Max(c => c.Company_ID) + 1 : 1;
            companies.Add(company);
            WriteAll(companies);
            return company.Company_ID;
        }

        public void Update(DAO.Company company)
        {
            var companies = ReadAll();
            var index = companies.FindIndex(c => c.Company_ID == company.Company_ID);
            if (index >= 0)
            {
                companies[index] = company;
                WriteAll(companies);
            }
        }

        public void Delete(int id)
        {
            var companies = ReadAll();
            companies.RemoveAll(c => c.Company_ID == id);
            WriteAll(companies);
        }

        public List<DAO.Company> GetByName(string name)
        {
            var companies = ReadAll();
            return companies.Where(c => c.Company_Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<DAO.Company> GetByStorage_ID(int storage_ID)
        {
            var companies = ReadAll();
            return companies.Where(c => c.Storage_ID == storage_ID).ToList();
        }
    }
}