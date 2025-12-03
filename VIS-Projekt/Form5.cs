using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form5 : Form
    {
        private StorageService storageService = null!;
        private CompanyService companyService = null!;
        private ProductService productService = null!;
        private StockService stockService = null!;
        private RequestService requestService = null!;

        private int selectedStorageId = -1;
        private int selectedCompanyId = -1;
        private int selectedProductId = -1;
        private bool isLoading = false;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            isLoading = true;

            storageService = new StorageService();
            companyService = new CompanyService();
            productService = new ProductService();
            stockService = new StockService();
            requestService = new RequestService();

            LoadStorages();
            RefreshRequestsList();

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

                LoadCompaniesForStorage();
                LoadProductsForStorage();
            }
            else
            {
                selectedStorageId = -1;
                ClearForm();
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

        private void LoadProductsForStorage()
        {
            try
            {
                if (selectedStorageId == -1) return;

                var products = productService.GetProductsByStorage(selectedStorageId);

                cbChooseProduct.DataSource = null;
                cbChooseProduct.Items.Clear();

                if (products.Count > 0)
                {
                    cbChooseProduct.DataSource = products;
                    cbChooseProduct.DisplayMember = "Name";
                    cbChooseProduct.ValueMember = "Product_ID";
                    cbChooseProduct.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání produktů: {ex.Message}");
            }
        }

        private void cbChooseCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbChooseCompany.SelectedIndex >= 0 && cbChooseCompany.SelectedItem != null)
            {
                var selectedCompany = (Company)cbChooseCompany.SelectedItem;
                selectedCompanyId = selectedCompany.Company_ID;
            }
            else
            {
                selectedCompanyId = -1;
            }
        }

        private void cbChooseProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (cbChooseProduct.SelectedIndex >= 0 && cbChooseProduct.SelectedItem != null)
            {
                var selectedProduct = (Product)cbChooseProduct.SelectedItem;
                selectedProductId = selectedProduct.Product_ID;

                UpdateAvailableQuantity();
            }
            else
            {
                selectedProductId = -1;
                lblAvailableQuantity.Text = "Dostupné Množství:";
            }
        }

        private void UpdateAvailableQuantity()
        {
            try
            {
                if (selectedProductId <= 0) return;

                int availableQuantity = stockService.GetAvailableQuantity(selectedProductId);
                lblAvailableQuantity.Text = $"Dostupné Množství: {availableQuantity} ks";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při zjišťování dostupnosti: {ex.Message}");
            }
        }

        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // Validace
                if (selectedStorageId == -1)
                {
                    MessageBox.Show("Vyberte sklad!");
                    return;
                }

                if (selectedCompanyId == -1)
                {
                    MessageBox.Show("Vyberte firmu!");
                    return;
                }

                if (selectedProductId == -1)
                {
                    MessageBox.Show("Vyberte produkt!");
                    return;
                }

                if (!int.TryParse(txtRequestQuantity.Text, out int requestedQuantity) || requestedQuantity <= 0)
                {
                    MessageBox.Show("Zadejte platné množství!");
                    return;
                }

                // Kontrola dostupnosti
                int availableQuantity = stockService.GetAvailableQuantity(selectedProductId);
                if (requestedQuantity > availableQuantity)
                {
                    MessageBox.Show($"Nedostatek zásob! Dostupné: {availableQuantity} ks");
                    return;
                }

                // Vytvoř objednávku S TRANSAKCÍ
                int requestId = requestService.CreateRequestWithAllocation(
                    selectedCompanyId,
                    selectedProductId,
                    requestedQuantity
                );

                MessageBox.Show($"Objednávka úspěšně vytvořena! ID: {requestId}\n\nZásoby byly alokovány.");

                // Refresh
                RefreshRequestsList();
                UpdateAvailableQuantity();
                ClearInputs();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}\n\nObjednávka nebyla vytvořena (ROLLBACK).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Neočekávaná chyba: {ex.Message}\n\nObjednávka nebyla vytvořena (ROLLBACK).");
            }
        }

        private void RefreshRequestsList()
        {
            try
            {
                var requests = requestService.GetAllRequests()
                    .OrderByDescending(r => r.Request_Date)
                    .Take(10)
                    .ToList();

                lstRequests.Items.Clear();

                foreach (var request in requests)
                {
                    var company = companyService.GetCompanyById(request.Company_ID);
                    var product = productService.GetProductById(request.Product_ID);

                    var companyName = company?.Company_Name ?? "Neznámá firma";
                    var productName = product?.Name ?? "Neznámý produkt";

                    lstRequests.Items.Add(
                        $"{request.Request_Date:dd.MM.yyyy HH:mm} - {companyName}: {productName} ({request.Request_Quantity} ks) - {request.Status}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání objednávek: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            cbChooseCompany.DataSource = null;
            cbChooseProduct.DataSource = null;
            lblAvailableQuantity.Text = "Dostupné Množství:";
            txtRequestQuantity.Clear();
        }

        private void ClearInputs()
        {
            txtRequestQuantity.Clear();
            cbChooseCompany.SelectedIndex = -1;
            cbChooseProduct.SelectedIndex = -1;
        }
    }
}