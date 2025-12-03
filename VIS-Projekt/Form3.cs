using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form3 : Form
    {
        private StorageService storageService = null!;
        private CompanyService companyService = null!;
        private SupplierService supplierService = null!;
        private ProductService productService = null!;
        private StockService stockService = null!;

        private int selectedStorageId = -1;
        private bool isLoading = false;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            isLoading = true;

            storageService = new StorageService();
            companyService = new CompanyService();
            supplierService = new SupplierService();
            productService = new ProductService();
            stockService = new StockService();

            LoadStorages();

            lstCompanyProducts.Items.Add("Vyberte sklad pro zobrazení možností Firem a Dodavatelů");
            lstSupplierProducts.Items.Add("Vyberte sklad pro zobrazení možností Firem a Dodavatelů");

            isLoading = false;
        }

        private void btnRequestForm_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
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

                LoadCompaniesForStorage();
                LoadSuppliersForStorage();
                ClearProductLists();
            }
            else
            {
                selectedStorageId = -1;
                cbChooseCompany.DataSource = null;
                cbChooseSupplier.DataSource = null;

                lstCompanyProducts.Items.Clear();
                lstSupplierProducts.Items.Clear();
                lstCompanyProducts.Items.Add("Vyberte sklad pro zobrazení možností Firem a Dodavatelů");
                lstSupplierProducts.Items.Add("Vyberte sklad pro zobrazení možností Firem a Dodavatelů");
            }
        }

        private void LoadCompaniesForStorage()
        {
            try
            {
                if (selectedStorageId == -1) return;

                var companies = companyService.GetCompaniesByStorage(selectedStorageId);

                cbChooseCompany.DataSource = null;
                cbChooseCompany.Items.Clear();

                if (companies.Count > 0)
                {
                    cbChooseCompany.DataSource = companies;
                    cbChooseCompany.DisplayMember = "Company_Name";
                    cbChooseCompany.ValueMember = "Company_ID";
                    cbChooseCompany.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání firem: {ex.Message}");
            }
        }

        private void LoadSuppliersForStorage()
        {
            try
            {
                if (selectedStorageId == -1) return;

                var suppliers = supplierService.GetSuppliersByStorage(selectedStorageId);

                cbChooseSupplier.DataSource = null;
                cbChooseSupplier.Items.Clear();

                if (suppliers.Count > 0)
                {
                    cbChooseSupplier.DataSource = suppliers;
                    cbChooseSupplier.DisplayMember = "Name";
                    cbChooseSupplier.ValueMember = "Supplier_ID";
                    cbChooseSupplier.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání dodavatelů: {ex.Message}");
            }
        }

        private void cbChooseCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbChooseCompany.SelectedIndex >= 0 && cbChooseCompany.SelectedItem != null)
            {
                var selectedCompany = (Company)cbChooseCompany.SelectedItem;
                LoadProductsForCompany(selectedCompany.Company_ID);
            }
            else
            {
                lstCompanyProducts.Items.Clear();
            }
        }

        private void cbChooseSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbChooseSupplier.SelectedIndex >= 0 && cbChooseSupplier.SelectedItem != null)
            {
                var selectedSupplier = (Supplier)cbChooseSupplier.SelectedItem;
                LoadProductsForSupplier(selectedSupplier.Supplier_ID);
            }
            else
            {
                lstSupplierProducts.Items.Clear();
            }
        }

        private void LoadProductsForCompany(int companyId)
        {
            try
            {
                lstCompanyProducts.Items.Clear();

                if (selectedStorageId == -1) return;

                var products = productService.GetProductsByStorage(selectedStorageId);

                if (products.Count == 0)
                {
                    lstCompanyProducts.Items.Add("Žádné produkty pro tento sklad");
                    return;
                }

                foreach (var product in products)
                {
                    var supplier = supplierService.GetSupplierById(product.Supplier_ID);
                    string supplierName = supplier?.Name ?? "Neznámý dodavatel";

                    var stocks = stockService.GetStocksByProductId(product.Product_ID);

                    if (stocks.Count == 0)
                    {
                        lstCompanyProducts.Items.Add(
                            $"{supplierName} | {product.Name} | {product.Type} | {product.CarModel} | {product.Price} Kč | - | 0 ks"
                        );
                    }
                    else
                    {
                        foreach (var stock in stocks)
                        {
                            if (stock.Storage_ID == selectedStorageId)
                            {
                                string location = string.IsNullOrWhiteSpace(stock.Location_In_Storage)
                                    ? "-"
                                    : stock.Location_In_Storage;

                                lstCompanyProducts.Items.Add(
                                    $"{supplierName} | {product.Name} | {product.Type} | {product.CarModel} | {product.Price} Kč | {location} | {stock.Quantity} ks"
                                );
                            }
                        }
                    }
                }

                if (lstCompanyProducts.Items.Count == 0)
                {
                    lstCompanyProducts.Items.Add("Žádné produkty pro tuto firmu ve vybraném skladu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání produktů pro firmu: {ex.Message}");
            }
        }

        private void LoadProductsForSupplier(int supplierId)
        {
            try
            {
                lstSupplierProducts.Items.Clear();

                if (selectedStorageId == -1) return;

                var products = productService.GetProductsBySupplier(supplierId);

                if (products.Count == 0)
                {
                    lstSupplierProducts.Items.Add("Žádné produkty od tohoto dodavatele");
                    return;
                }

                var storageProducts = products.Where(p => p.Storage_ID == selectedStorageId).ToList();

                if (storageProducts.Count == 0)
                {
                    lstSupplierProducts.Items.Add("Žádné produkty od tohoto dodavatele ve vybraném skladu");
                    return;
                }

                foreach (var product in storageProducts)
                {
                    var companies = companyService.GetCompaniesByStorage(selectedStorageId);
                    string companyInfo = companies.Count > 0 ? companies[0].Company_Name : "Různé firmy";

                    var stocks = stockService.GetStocksByProductId(product.Product_ID);

                    if (stocks.Count == 0)
                    {
                        lstSupplierProducts.Items.Add(
                            $"{companyInfo} | {product.Name} | {product.Type} | {product.CarModel} | {product.Price} Kč | - | 0 ks"
                        );
                    }
                    else
                    {
                        foreach (var stock in stocks)
                        {
                            if (stock.Storage_ID == selectedStorageId)
                            {
                                string location = string.IsNullOrWhiteSpace(stock.Location_In_Storage)
                                    ? "-"
                                    : stock.Location_In_Storage;

                                lstSupplierProducts.Items.Add(
                                    $"{companyInfo} | {product.Name} | {product.Type} | {product.CarModel} | {product.Price} Kč | {location} | {stock.Quantity} ks"
                                );
                            }
                        }
                    }
                }

                if (lstSupplierProducts.Items.Count == 0)
                {
                    lstSupplierProducts.Items.Add("Žádné produkty od tohoto dodavatele ve vybraném skladu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání produktů pro dodavatele: {ex.Message}");
            }
        }

        private void ClearProductLists()
        {
            lstCompanyProducts.Items.Clear();
            lstSupplierProducts.Items.Clear();
        }
    }
}