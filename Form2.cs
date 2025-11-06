using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form2 : Form
    {
        private CompanyService companyService = null!;
        private ProductService productService = null!;
        private int selectedCompanyId = -1;

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
                if (comboBoxCompanies.SelectedValue != null &&
                    int.TryParse(comboBoxCompanies.SelectedValue.ToString(), out int companyId))
                {
                    selectedCompanyId = companyId;
                    LoadProductsForCompany(companyId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void LoadProductsForCompany(int companyId)
        {
            try
            {
                var allProducts = productService.GetAllProducts();

                // Filtrujeme produkty podle Company_ID
                // POKUD Products nemají Company_ID, pak zobrazíme všechny produkty
                var productsForCompany = allProducts
                    .Where(p => p.CompanyID == companyId)
                    .ToList();

                lstProducts.Items.Clear();

                if (productsForCompany.Count == 0)
                {
                    lstProducts.Items.Add("Žádné produkty pro tohoto zákazníka");
                }
                else
                {
                    foreach (var product in productsForCompany)
                    {
                        lstProducts.Items.Add(
                            $"{product.Product_ID} - {product.Name} | {product.Type} | {product.CarModel} | Cena: {product.Price} Kč"
                        );
                    }
                }

                labelProductCount.Text = $"Počet produktů: {productsForCompany.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání produktů: {ex.Message}");
            }
        }

        private void lstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Zatím nic - můžeš tu přidat detaily produktu pokud chceš
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}