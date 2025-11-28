namespace Domain.Mappers
{
    public static class RequestMapper
    {
        public static Models.Request FromDAO(DataAccess.DAO.Request daoRequest)
        {
            if (daoRequest == null) return null;

            return new Models.Request
            {
                Request_ID = daoRequest.Request_ID,
                Company_ID = daoRequest.Company_ID,
                Product_ID = daoRequest.Product_ID,
                Request_Quantity = daoRequest.Request_Quantity,
                Request_Date = daoRequest.Request_Date,
                Status = daoRequest.Status
            };
        }

        public static DataAccess.DAO.Request ToDAO(Models.Request domainRequest)
        {
            if (domainRequest == null) return null;

            return new DataAccess.DAO.Request
            {
                Request_ID = domainRequest.Request_ID,
                Company_ID = domainRequest.Company_ID,
                Product_ID = domainRequest.Product_ID,
                Request_Quantity = domainRequest.Request_Quantity,
                Request_Date = domainRequest.Request_Date,
                Status = domainRequest.Status
            };
        }

        public static List<Models.Request> FromDAOList(List<DataAccess.DAO.Request> daoRequests)
        {
            return daoRequests?.Select(FromDAO).ToList() ?? new List<Models.Request>();
        }

        public static List<DataAccess.DAO.Request> ToDAOList(List<Models.Request> domainRequests)
        {
            return domainRequests?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.Request>();
        }
    }
}