using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form1 : Form
    {
        private CompanyService companyService = null!;
        private SupplierService supplierService = null!;
        private ProductService productService = null!;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            companyService = new CompanyService();
            supplierService = new SupplierService();
            productService = new ProductService();

            RefreshCompanyList();
            RefreshSupplierList();
            RefreshProductList();
        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    MessageBox.Show("Zadej název firmy!");
                    return;
                }

                var newCompany = new Company
                {
                    Company_Name = txtCompanyName.Text,
                    Contact_Email = txtEmail.Text,
                    Contact_Phone = txtPhone.Text
                };

                int id = companyService.CreateCompany(newCompany);
                MessageBox.Show($"Firma přidána! ID: {id}");

                RefreshCompanyList();
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
                if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
                {
                    MessageBox.Show("Zadej název výrobce!");
                    return;
                }

                var newSupplier = new Supplier
                {
                    Name = txtSupplierName.Text,
                    Phone = txtSupplierPhone.Text,
                    Email = txtSupplierEmail.Text
                };

                int id = supplierService.CreateSupplier(newSupplier);
                MessageBox.Show($"Výrobce přidán! ID: {id}");

                RefreshSupplierList();
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
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Zadej název produktu!");
                    return;
                }

                if (!int.TryParse(txtSupplierId.Text, out int supplierId))
                {
                    MessageBox.Show("Zadej platné ID výrobce!");
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Zadej platnou cenu!");
                    return;
                }

                var newProduct = new Product
                {
                    Name = txtProductName.Text,
                    Type = txtProductType.Text,
                    CarModel = txtCarModel.Text,
                    Supplier_ID = supplierId,
                    Price = price
                };

                int id = productService.CreateProduct(newProduct);
                MessageBox.Show($"Produkt přidán! ID: {id}");

                RefreshProductList();
                ClearProductForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void RefreshCompanyList()
        {
            try
            {
                var companies = companyService.GetAllCompanies();
                lstCompanies.Items.Clear();
                foreach (var company in companies)
                {
                    lstCompanies.Items.Add(
                        $"{company.Company_ID} - {company.Company_Name} | {company.Contact_Phone} | {company.Contact_Email}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání: {ex.Message}");
            }
        }

        private void RefreshSupplierList()
        {
            try
            {
                var suppliers = supplierService.GetAllSuppliers();
                lstSuppliers.Items.Clear();
                foreach (var supplier in suppliers)
                {
                    lstSuppliers.Items.Add(
                        $"{supplier.Supplier_ID} - {supplier.Name} | {supplier.Phone} | {supplier.Email}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání: {ex.Message}");
            }
        }

        private void RefreshProductList()
        {
            try
            {
                var products = productService.GetAllProducts();
                lstProducts.Items.Clear();
                foreach (var product in products)
                {
                    lstProducts.Items.Add(
                        $"{product.Product_ID} - {product.Name} | {product.Type} | {product.CarModel} | {product.Supplier_ID} | ({product.Price} Kč)"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání: {ex.Message}");
            }
        }

        private void ClearCompanyForm()
        {
            txtCompanyName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
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
            txtCarModel.Clear();
            txtSupplierId.Clear();
            txtPrice.Clear();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
