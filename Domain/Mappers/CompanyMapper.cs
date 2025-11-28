namespace Domain.Mappers
{
    public static class CompanyMapper
    {
        public static Models.Company FromDAO(DataAccess.DAO.Company daoCompany)
        {
            if (daoCompany == null) return null;

            return new Models.Company
            {
                Company_ID = daoCompany.Company_ID,
                Company_Name = daoCompany.Company_Name,
                Contact_Email = daoCompany.Contact_Email,
                Contact_Phone = daoCompany.Contact_Phone,
                Storage_ID = daoCompany.Storage_ID
            };
        }

        public static DataAccess.DAO.Company ToDAO(Models.Company domainCompany)
        {
            if (domainCompany == null) return null;

            return new DataAccess.DAO.Company
            {
                Company_ID = domainCompany.Company_ID,
                Company_Name = domainCompany.Company_Name,
                Contact_Email = domainCompany.Contact_Email,
                Contact_Phone = domainCompany.Contact_Phone,
                Storage_ID = domainCompany.Storage_ID
            };
        }

        public static List<Models.Company> FromDAOList(List<DataAccess.DAO.Company> daoCompanies)
        {
            return daoCompanies?.Select(FromDAO).ToList() ?? new List<Models.Company>();
        }

        public static List<DataAccess.DAO.Company> ToDAOList(List<Models.Company> domainCompanies)
        {
            return domainCompanies?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.Company>();
        }
    }
}