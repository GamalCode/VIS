using System.Data;
using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form2 : Form
    {
        private CompanyService companyService = null!;
        private ProductService productService = null!;
        private int selectedCompanyId = -1;
        private string selectedCompanyName = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                companyService = new CompanyService();
                productService = new ProductService();

                comboBoxCompanies.SelectedIndex = -1;

                LoadCompanies();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při inicializaci: {ex.Message}");
            }
        }

        private void LoadCompanies()
        {
            try
            {
                var companies = companyService.GetAllCompanies();
                comboBoxCompanies.Items.Clear();
                comboBoxCompanies.DataSource = companies;
                comboBoxCompanies.DisplayMember = "Company_Name";
                comboBoxCompanies.ValueMember = "Company_ID";
                comboBoxCompanies.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání zákazníků: {ex.Message}");
            }
        }

        private void comboBoxCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCompanies.SelectedIndex < 0)
                {
                    lstProducts.Items.Clear();
                    labelProductCount.Text = "Počet produktů: 0";
                    return;
                }

                if (comboBoxCompanies.SelectedValue is int companyId)
                {
                    var selectedCompany = comboBoxCompanies.SelectedItem as Company;
                    if (selectedCompany != null)
                    {
                        selectedCompanyId = companyId;
                        selectedCompanyName = selectedCompany.Company_Name;
                        LoadProductsForCompany();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void LoadProductsForCompany()
        {
            try
            {
                lstProducts.Items.Clear();

                var allProducts = productService.GetAllProducts();
                var filteredProducts = allProducts
                    .Where(p => !string.IsNullOrEmpty(p.CarModel) &&
                                p.CarModel.Contains(selectedCompanyName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filteredProducts.Count == 0)
                {
                    lstProducts.Items.Add($"Žádné produkty pro zákazníka '{selectedCompanyName}'");
                    labelProductCount.Text = "Počet produktů: 0";
                    return;
                }

                foreach (var product in filteredProducts)
                {
                    lstProducts.Items.Add(
                        $"{product.Product_ID} - {product.Name} | {product.Type} | {product.CarModel} | Cena: {product.Price} Kč"
                    );
                }

                labelProductCount.Text = $"Počet produktů: {filteredProducts.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání produktů: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}