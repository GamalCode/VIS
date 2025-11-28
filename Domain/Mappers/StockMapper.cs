namespace Domain.Mappers
{
    public static class StockMapper
    {
        public static Models.Stock FromDAO(DataAccess.DAO.Stock daoStock)
        {
            if (daoStock == null) return null;

            return new Models.Stock
            {
                Stock_ID = daoStock.Stock_ID,
                Product_ID = daoStock.Product_ID,
                Storage_ID = daoStock.Storage_ID,
                Quantity = daoStock.Quantity,
                Location_In_Storage = daoStock.Location_In_Storage
            };
        }

        public static DataAccess.DAO.Stock ToDAO(Models.Stock domainStock)
        {
            if (domainStock == null) return null;

            return new DataAccess.DAO.Stock
            {
                Stock_ID = domainStock.Stock_ID,
                Product_ID = domainStock.Product_ID,
                Storage_ID = domainStock.Storage_ID,
                Quantity = domainStock.Quantity,
                Location_In_Storage = domainStock.Location_In_Storage
            };
        }

        public static List<Models.Stock> FromDAOList(List<DataAccess.DAO.Stock> daoStocks)
        {
            return daoStocks?.Select(FromDAO).ToList() ?? new List<Models.Stock>();
        }

        public static List<DataAccess.DAO.Stock> ToDAOList(List<Models.Stock> domainStocks)
        {
            return domainStocks?.Select(ToDAO).ToList() ?? new List<DataAccess.DAO.Stock>();
        }
    }
}