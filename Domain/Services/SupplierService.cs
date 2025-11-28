using DataAccess.GlobalConfig;
using Domain.Mappers;
using Domain.Models;

namespace Domain.Services
{
    public class SupplierService
    {
        public List<Supplier> GetAllSuppliers()
        {
            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            var daoSuppliers = supplierDAO.GetAll();
            return SupplierMapper.FromDAOList(daoSuppliers);
        }

        public Supplier GetSupplierById(int id)
        {
            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            var daoSupplier = supplierDAO.GetById(id);
            return SupplierMapper.FromDAO(daoSupplier);
        }

        public List<Supplier> GetSuppliersByName(string name)
        {
            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            var daoSuppliers = supplierDAO.GetByName(name);
            return SupplierMapper.FromDAOList(daoSuppliers);
        }

        public List<Supplier> GetSuppliersByStorage(int storageId)
        {
            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            var daoSuppliers = supplierDAO.GetByStorage_ID(storageId);
            return SupplierMapper.FromDAOList(daoSuppliers);
        }

        public int CreateSupplier(Supplier supplier)
        {
            ValidateSupplier(supplier);

            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            var daoSupplier = SupplierMapper.ToDAO(supplier);
            return supplierDAO.Insert(daoSupplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            ValidateSupplier(supplier);

            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            var daoSupplier = SupplierMapper.ToDAO(supplier);
            supplierDAO.Update(daoSupplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            var supplierDAO = GlobalConfig.Connection.GetSupplierDAO();
            supplierDAO.Delete(supplierId);
        }

        private void ValidateSupplier(Supplier supplier)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            if (string.IsNullOrWhiteSpace(supplier.Name))
                throw new ArgumentException("Jméno dodavatele nemůže být prázdné.");

            if (supplier.Storage_ID <= 0)
                throw new ArgumentException("Sklad musí být vybrán.");
        }
    }
}