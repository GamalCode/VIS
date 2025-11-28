using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class RequestStockService
    {
        public List<RequestStock> GetAllRequestStocks()
        {
            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            var daoRequestStocks = requestStockDAO.GetAll();
            return RequestStockMapper.FromDAOList(daoRequestStocks);
        }

        public RequestStock GetRequestStockById(int id)
        {
            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            var daoRequestStock = requestStockDAO.GetById(id);
            return RequestStockMapper.FromDAO(daoRequestStock);
        }

        public List<RequestStock> GetRequestStocksByRequestId(int requestId)
        {
            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            var daoRequestStocks = requestStockDAO.GetByRequest_ID(requestId);
            return RequestStockMapper.FromDAOList(daoRequestStocks);
        }

        public List<RequestStock> GetRequestStocksByStockId(int stockId)
        {
            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            var daoRequestStocks = requestStockDAO.GetByStock_ID(stockId);
            return RequestStockMapper.FromDAOList(daoRequestStocks);
        }

        public int CreateRequestStock(RequestStock requestStock)
        {
            ValidateRequestStock(requestStock);

            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            var daoRequestStock = RequestStockMapper.ToDAO(requestStock);
            return requestStockDAO.Insert(daoRequestStock);
        }

        public void UpdateRequestStock(RequestStock requestStock)
        {
            ValidateRequestStock(requestStock);

            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            var daoRequestStock = RequestStockMapper.ToDAO(requestStock);
            requestStockDAO.Update(daoRequestStock);
        }

        public void DeleteRequestStock(int requestStockId)
        {
            var requestStockDAO = GlobalConfig.Connection.GetRequestStockDAO();
            requestStockDAO.Delete(requestStockId);
        }

        public int GetTotalAllocatedQuantity(int requestId)
        {
            var requestStocks = GetRequestStocksByRequestId(requestId);
            int total = 0;

            foreach (var rs in requestStocks)
            {
                if (rs.Allocated_Quantity.HasValue)
                {
                    total += rs.Allocated_Quantity.Value;
                }
            }

            return total;
        }

        public bool IsRequestFullyAllocated(int requestId)
        {
            var requestService = new RequestService();
            var request = requestService.GetRequestById(requestId);
            if (request == null) return false;

            int allocated = GetTotalAllocatedQuantity(requestId);
            return allocated >= request.Request_Quantity;
        }

        public string GetAllocationInfo(int requestStockId)
        {
            var requestStock = GetRequestStockById(requestStockId);
            if (requestStock == null) return "Neznámé přidělení";

            var requestService = new RequestService();
            var stockService = new StockService();

            var request = requestService.GetRequestById(requestStock.Request_ID.GetValueOrDefault());
            var stock = stockService.GetStockById(requestStock.Stock_ID.GetValueOrDefault());

            var requestInfo = request != null ? request.Request_ID.ToString() : "Neznámá objednávka";
            var stockInfo = stock != null ? stock.Stock_ID.ToString() : "Neznámá zásoba";

            return $"Objednávka {requestInfo} - Zásoba {stockInfo} - {requestStock.Allocated_Quantity} ks";
        }

        private void ValidateRequestStock(RequestStock requestStock)
        {
            if (requestStock == null)
                throw new ArgumentNullException(nameof(requestStock));

            if (requestStock.Request_ID.HasValue && requestStock.Request_ID <= 0)
                throw new ArgumentException("Objednávka musí být validní.");

            if (requestStock.Stock_ID.HasValue && requestStock.Stock_ID <= 0)
                throw new ArgumentException("Zásoba musí být validní.");

            if (requestStock.Allocated_Quantity.HasValue && requestStock.Allocated_Quantity < 0)
                throw new ArgumentException("Přidělené množství nemůže být záporné.");
        }
    }
}