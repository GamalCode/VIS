using DataAccess.GlobalConfig;
using Domain.Mappers;

namespace Domain.Services
{
    public class RequestService
    {
        public int CreateRequestWithAllocation(int companyId, int productId, int requestedQuantity)
        {
            using (var uow = GlobalConfig.UnitOfWorkFactory.Create())
            {
                try
                {
                    uow.BeginTransaction();

                    var request = new DataAccess.DAO.Request
                    {
                        Company_ID = companyId,
                        Product_ID = productId,
                        Request_Quantity = requestedQuantity,
                        Request_Date = DateTime.Now,
                        Status = "Pending"
                    };

                    int requestId = uow.Requests.Insert(request);

                    var availableStocks = uow.Stocks.GetByProduct_ID(productId)
                        .Where(s => s.Quantity > 0)
                        .OrderBy(s => s.Stock_ID)
                        .ToList();

                    int remainingQuantity = requestedQuantity;

                    foreach (var stock in availableStocks)
                    {
                        if (remainingQuantity <= 0) break;

                        int toAllocate = Math.Min(stock.Quantity, remainingQuantity);

                        var requestStock = new DataAccess.DAO.RequestStock
                        {
                            Request_ID = requestId,
                            Stock_ID = stock.Stock_ID,
                            Allocated_Quantity = toAllocate
                        };
                        uow.RequestStocks.Insert(requestStock);

                        stock.Quantity -= toAllocate;
                        uow.Stocks.Update(stock);

                        remainingQuantity -= toAllocate;
                    }

                    if (remainingQuantity > 0)
                    {
                        throw new InvalidOperationException(
                            $"Nedostatek zásob! Chybí ještě {remainingQuantity} ks."
                        );
                    }

                    request.Request_ID = requestId;
                    request.Status = "Allocated";
                    uow.Requests.Update(request);

                    uow.Commit();

                    return requestId;
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }

        public List<Models.Request> GetAllRequests()
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequests = requestDAO.GetAll();
            return RequestMapper.FromDAOList(daoRequests);
        }

        public Models.Request GetRequestById(int id)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequest = requestDAO.GetById(id);
            return RequestMapper.FromDAO(daoRequest);
        }

        public List<Models.Request> GetRequestsByCompanyId(int companyId)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequests = requestDAO.GetByCompany_ID(companyId);
            return RequestMapper.FromDAOList(daoRequests);
        }

        public List<Models.Request> GetRequestsByStatus(string status)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequests = requestDAO.GetByStatus(status);
            return RequestMapper.FromDAOList(daoRequests);
        }

        public void UpdateRequest(Models.Request request)
        {
            ValidateRequest(request);

            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequest = RequestMapper.ToDAO(request);
            requestDAO.Update(daoRequest);
        }

        public void DeleteRequest(int requestId)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            requestDAO.Delete(requestId);
        }

        public bool CanFulfillRequest(int requestId)
        {
            var request = GetRequestById(requestId);
            if (request == null) return false;

            var stockService = new StockService();
            return stockService.HasSufficientStock(request.Product_ID, request.Request_Quantity);
        }

        public string GetRequestStatus(int requestId)
        {
            var request = GetRequestById(requestId);
            if (request == null) return "Neznámá objednávka";

            var companyService = new CompanyService();
            var productService = new ProductService();

            var company = companyService.GetCompanyById(request.Company_ID);
            var product = productService.GetProductById(request.Product_ID);

            var companyName = company?.Company_Name ?? "Neznámá společnost";
            var productName = product?.Name ?? "Neznámý produkt";

            return $"{companyName} - {productName} ({request.Request_Quantity} ks) - {request.Status}";
        }

        public int GetRequestsCount(string status)
        {
            var requests = GetRequestsByStatus(status);
            return requests.Count;
        }

        private void ValidateRequest(Models.Request request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Company_ID <= 0)
                throw new ArgumentException("Společnost musí být vybrána.");

            if (request.Product_ID <= 0)
                throw new ArgumentException("Produkt musí být vybrán.");

            if (request.Request_Quantity <= 0)
                throw new ArgumentException("Množství musí být větší než 0.");

            if (request.Request_Date == default)
                throw new ArgumentException("Datum objednávky musí být nastaveno.");
        }
    }
}