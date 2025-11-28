using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form2 : Form
    {
        private StorageService storageService = null!;
        private CompanyService companyService = null!;
        private SupplierService supplierService = null!;
        private ProductService productService = null!;
        private StockService stockService = null!;

        private int selectedStorageId = -1;
        private bool isLoading = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            isLoading = true;

            storageService = new StorageService();
            companyService = new CompanyService();
            supplierService = new SupplierService();
            productService = new ProductService();
            stockService = new StockService();

            LoadStorages();
            RefreshAllLists();

            isLoading = false;
        }

        private void LoadStorages()
        {
            try
            {
                var storages = storageService.GetAllStorages();

                cbChooseStorage.DataSource = null;
                cbChooseStorage.Items.Clear();

                cbChooseStorage.DataSource = storages;
                cbChooseStorage.DisplayMember = "Storage_Location";
                cbChooseStorage.ValueMember = "Storage_ID";
                cbChooseStorage.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání skladů: {ex.Message}");
            }
        }

        private void cbChooseStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbChooseStorage.SelectedIndex >= 0 && cbChooseStorage.SelectedItem != null)
            {
                var selectedStorage = (Storage)cbChooseStorage.SelectedItem;
                selectedStorageId = selectedStorage.Storage_ID;

                txtStockStorageID.Text = selectedStorageId.ToString();

                RefreshListForSelectedStorage();
            }
            else
            {
                selectedStorageId = -1;
                txtStockStorageID.Clear();
                RefreshListForSelectedStorage();
            }
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStorageId == -1)
                {
                    MessageBox.Show("Nejprve vyber sklad!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    MessageBox.Show("Zadej název firmy!");
                    return;
                }

                var newCompany = new Company
                {
                    Company_Name = txtCompanyName.Text,
                    Contact_Email = txtCompanyEmail.Text,
                    Contact_Phone = txtCompanyPhone.Text,
                    Storage_ID = selectedStorageId
                };

                int id = companyService.CreateCompany(newCompany);
                MessageBox.Show($"Firma přidána! ID: {id}");

                RefreshListForSelectedStorage();
                ClearCompanyForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStorageId == -1)
                {
                    MessageBox.Show("Nejprve vyber sklad!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
                {
                    MessageBox.Show("Zadej název dodavatele!");
                    return;
                }

                var newSupplier = new Supplier
                {
                    Name = txtSupplierName.Text,
                    Phone = txtSupplierPhone.Text,
                    Email = txtSupplierEmail.Text,
                    Storage_ID = selectedStorageId
                };

                int id = supplierService.CreateSupplier(newSupplier);
                MessageBox.Show($"Dodavatel přidán! ID: {id}");

                RefreshListForSelectedStorage();
                ClearSupplierForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStorageId == -1)
                {
                    MessageBox.Show("Nejprve vyber sklad!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Zadej název produktu!");
                    return;
                }

                if (!int.TryParse(txtProductSupplierID.Text, out int supplierId))
                {
                    MessageBox.Show("Zadej platné ID dodavatele!");
                    return;
                }

                var suppliers = supplierService.GetSuppliersByStorage(selectedStorageId);
                var supplier = suppliers.FirstOrDefault(s => s.Supplier_ID == supplierId);

                if (supplier == null)
                {
                    MessageBox.Show($"Dodavatel s ID {supplierId} neexistuje v tomto skladu!");
                    return;
                }

                if (!decimal.TryParse(txtProductPrice.Text, out decimal price))
                {
                    MessageBox.Show("Zadej platnou cenu!");
                    return;
                }

                var newProduct = new Product
                {
                    Name = txtProductName.Text,
                    Type = txtProductType.Text,
                    CarModel = txtProductModelCar.Text,
                    Supplier_ID = supplierId,
                    Price = price,
                    Storage_ID = selectedStorageId
                };

                int id = productService.CreateProduct(newProduct);
                MessageBox.Show($"Produkt přidán! ID: {id}");

                RefreshListForSelectedStorage();
                ClearProductForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedStorageId == -1)
                {
                    MessageBox.Show("Nejprve vyber sklad!");
                    return;
                }

                if (!int.TryParse(txtStockProductID.Text, out int productId))
                {
                    MessageBox.Show("Zadej platné ID produktu!");
                    return;
                }

                var products = productService.GetProductsByStorage(selectedStorageId);
                var product = products.FirstOrDefault(p => p.Product_ID == productId);

                if (product == null)
                {
                    MessageBox.Show($"Produkt s ID {productId} neexistuje v tomto skladu!");
                    return;
                }

                if (!int.TryParse(txtStockQuantity.Text, out int quantity))
                {
                    MessageBox.Show("Zadej platné množství!");
                    return;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show("Množství musí být větší než 0!");
                    return;
                }

                var newStock = new Stock
                {
                    Product_ID = productId,
                    Storage_ID = selectedStorageId,
                    Quantity = quantity,
                    Location_In_Storage = txtStockLocationInStorage.Text
                };

                int id = stockService.CreateStock(newStock);
                MessageBox.Show($"Stock přidán! ID: {id}");

                RefreshListForSelectedStorage();
                ClearStockForm();

                UpdateStorageLastUpdated(selectedStorageId);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void RefreshAllLists()
        {
            RefreshCompanyList();
            RefreshSupplierList();
            RefreshProductList();
            RefreshListForSelectedStorage();
        }

        private void RefreshCompanyList()
        {
            try
            {
                var companies = companyService.GetAllCompanies();
                lstCompany.Items.Clear();

                if (companies.Count == 0)
                {
                    return;
                }

                foreach (var company in companies)
                {
                    lstCompany.Items.Add(
                        $"{company.Company_ID} - {company.Company_Name} | {company.Contact_Phone} | {company.Contact_Email}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání firem: {ex.Message}");
            }
        }

        private void RefreshSupplierList()
        {
            try
            {
                var suppliers = supplierService.GetAllSuppliers();
                lstSupplier.Items.Clear();

                if (suppliers.Count == 0)
                {
                    return;
                }

                foreach (var supplier in suppliers)
                {
                    lstSupplier.Items.Add(
                        $"{supplier.Supplier_ID} - {supplier.Name} | {supplier.Phone} | {supplier.Email}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání dodavatelů: {ex.Message}");
            }
        }

        private void RefreshProductList()
        {
            try
            {
                var products = productService.GetAllProducts();
                lstProduct.Items.Clear();

                if (products.Count == 0)
                {
                    return;
                }

                foreach (var product in products)
                {
                    lstProduct.Items.Add(
                        $"{product.Product_ID} - {product.Name} | {product.Type} | {product.CarModel} | {product.Price} Kč"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání produktů: {ex.Message}");
            }
        }

        private void RefreshListForSelectedStorage()
        {
            try
            {
                lstCompany.Items.Clear();
                lstSupplier.Items.Clear();
                lstProduct.Items.Clear();
                lstStock.Items.Clear();

                if (selectedStorageId == -1)
                {
                    lstCompany.Items.Add("Vyberte sklad pro zobrazení dat");
                    lstSupplier.Items.Add("Vyberte sklad pro zobrazení dat");
                    lstProduct.Items.Add("Vyberte sklad pro zobrazení dat");
                    lstStock.Items.Add("Vyberte sklad pro zobrazení dat");
                    return;
                }

                var companies = companyService.GetCompaniesByStorage(selectedStorageId);
                foreach (var company in companies)
                {
                    lstCompany.Items.Add(
                        $"{company.Company_ID} - {company.Company_Name} | {company.Contact_Phone} | {company.Contact_Email}"
                    );
                }

                var suppliers = supplierService.GetSuppliersByStorage(selectedStorageId);
                foreach (var supplier in suppliers)
                {
                    lstSupplier.Items.Add(
                        $"{supplier.Supplier_ID} - {supplier.Name} | {supplier.Phone} | {supplier.Email}"
                    );
                }

                var products = productService.GetProductsByStorage(selectedStorageId);
                foreach (var product in products)
                {
                    lstProduct.Items.Add(
                        $"{product.Product_ID} - {product.Name} | {product.Type} | {product.CarModel} | {product.Price} Kč"
                    );
                }

                var stocks = stockService.GetStocksByStorageId(selectedStorageId);
                foreach (var stock in stocks)
                {
                    var product = productService.GetProductById(stock.Product_ID);
                    var productName = product?.Name ?? "Neznámý produkt";

                    lstStock.Items.Add(
                        $" {stock.Stock_ID} - {productName} | {stock.Quantity} ks | {stock.Location_In_Storage}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání dat: {ex.Message}");
            }
        }

        private void ClearCompanyForm()
        {
            txtCompanyName.Clear();
            txtCompanyEmail.Clear();
            txtCompanyPhone.Clear();
        }

        private void ClearSupplierForm()
        {
            txtSupplierName.Clear();
            txtSupplierPhone.Clear();
            txtSupplierEmail.Clear();
        }

        private void ClearProductForm()
        {
            txtProductName.Clear();
            txtProductType.Clear();
            txtProductModelCar.Clear();
            txtProductSupplierID.Clear();
            txtProductPrice.Clear();
        }

        private void ClearStockForm()
        {
            txtStockProductID.Clear();
            txtStockQuantity.Clear();
            txtStockLocationInStorage.Clear();
        }

        private void UpdateStorageLastUpdated(int storageId)
        {
            try
            {
                var storage = storageService.GetStorageById(storageId);
                if (storage != null)
                {
                    storage.Last_Updated = DateTime.Now;
                    storageService.UpdateStorage(storage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nepodařilo se aktualizovat Last_Updated: {ex.Message}");
            }
        }
    }
}