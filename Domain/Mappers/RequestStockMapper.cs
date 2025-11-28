namespace Domain.Mappers
{
    public static class RequestStockMapper
    {
        public static Models.RequestStock FromDAO(DataAccess.DAO.RequestStock daoRequestStock)
        {
            if (daoRequestStock == null) return null;

            return new Models.RequestStock
            {
                Request_Stock_ID = daoRequestStock.Request_Stock_ID,
                Request_ID = daoRequestStock.Request_ID,
                Stock_ID = daoRequestStock.Stock_ID,
                Allocated_Quantity = daoRequestStock.Allocated_Quantity
            };
        }

        public static DataAccess.DAO.RequestStock ToDAO(Models.RequestStock domainRequestStock)
        {
            if (domainRequestStock == null) return null;

            return new DataAccess.DAO.RequestStock
            {
                Request_Stock_ID = domainRequestStock.Request_Stock_ID,
                Request_ID = domainRequestStock.Request_ID,
                Stock_ID = domainRequestStock.Stock_ID,
                Allocated_Quantity = domainRequestStock.Allocated_Quantity
            };
        }

        public static List<Models.RequestStock> FromDAOList(List<DataAccess.DAO.RequestStock> daoRequestStocks)
        {
            return daoRequestStocks?.Select(FromDAO).ToList() ?? new List<Models.RequestStock>();
        }

        public static List<DataAccess.DAO.RequestStock> ToDAOList(List<Models.RequestStock> domainRequestStocks)
        {
            return domainRequestStocks?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.RequestStock>();
        }
    }
}