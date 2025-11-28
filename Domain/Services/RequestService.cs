using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class RequestService
    {
        public List<Request> GetAllRequests()
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequests = requestDAO.GetAll();
            return RequestMapper.FromDAOList(daoRequests);
        }

        public Request GetRequestById(int id)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequest = requestDAO.GetById(id);
            return RequestMapper.FromDAO(daoRequest);
        }

        public List<Request> GetRequestsByCompanyId(int companyId)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequests = requestDAO.GetByCompany_ID(companyId);
            return RequestMapper.FromDAOList(daoRequests);
        }

        public List<Request> GetRequestsByStatus(string status)
        {
            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequests = requestDAO.GetByStatus(status);
            return RequestMapper.FromDAOList(daoRequests);
        }

        public int CreateRequest(Request request)
        {
            ValidateRequest(request);

            var requestDAO = GlobalConfig.Connection.GetRequestDAO();
            var daoRequest = RequestMapper.ToDAO(request);
            return requestDAO.Insert(daoRequest);
        }

        public void UpdateRequest(Request request)
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

        private void ValidateRequest(Request request)
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