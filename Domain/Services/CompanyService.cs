using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class CompanyService
    {
        public List<Company> GetAllCompanies()
        {
            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            var daoCompanies = companyDAO.GetAll();
            return CompanyMapper.FromDAOList(daoCompanies);
        }

        public Company GetCompanyById(int id)
        {
            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            var daoCompany = companyDAO.GetById(id);
            return CompanyMapper.FromDAO(daoCompany);
        }

        public List<Company> GetCompaniesByName(string name)
        {
            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            var daoCompanies = companyDAO.GetByName(name);
            return CompanyMapper.FromDAOList(daoCompanies);
        }

        public List<Company> GetCompaniesByStorage(int storageId)
        {
            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            var daoCompanies = companyDAO.GetByStorage_ID(storageId);
            return CompanyMapper.FromDAOList(daoCompanies);
        }

        public int CreateCompany(Company company)
        {
            ValidateCompany(company);

            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            var daoCompany = CompanyMapper.ToDAO(company);
            return companyDAO.Insert(daoCompany);
        }

        public void UpdateCompany(Company company)
        {
            ValidateCompany(company);

            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            var daoCompany = CompanyMapper.ToDAO(company);
            companyDAO.Update(daoCompany);
        }

        public void DeleteCompany(int companyId)
        {
            var companyDAO = GlobalConfig.Connection.GetCompanyDAO();
            companyDAO.Delete(companyId);
        }

        public int GetTotalRequests(int companyId)
        {
            var requestService = new RequestService();
            var requests = requestService.GetRequestsByCompanyId(companyId);
            return requests.Count;
        }

        public int GetPendingRequests(int companyId)
        {
            var requestService = new RequestService();
            var requests = requestService.GetRequestsByCompanyId(companyId);

            int pending = 0;
            foreach (var request in requests)
            {
                if (request.Status == "Pending" || request.Status == "pending")
                {
                    pending++;
                }
            }

            return pending;
        }

        public string GetCompanyInfo(int companyId)
        {
            var company = GetCompanyById(companyId);
            if (company == null) return "Neznámá společnost";

            return $"{company.Company_Name} - {company.Contact_Email} - {company.Contact_Phone}";
        }

        private void ValidateCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            if (string.IsNullOrWhiteSpace(company.Company_Name))
                throw new ArgumentException("Název společnosti nemůže být prázdný.");

            if (company.Storage_ID <= 0)
                throw new ArgumentException("Sklad musí být vybrán.");
        }
    }
}