using Domain.Services;
using Domain.Models;

namespace VIS_Projekt
{
    public partial class Form1 : Form
    {
        private StorageService storageService = null!;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            storageService = new StorageService();
            RefreshStorageList();
        }

        private void btnAddStorage_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStorageLocation.Text))
                {
                    MessageBox.Show("Zadej lokaci skladu!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtStorageCapacity.Text))
                {
                    MessageBox.Show("Zadej kapacitu skladu!");
                    return;
                }

                if (!int.TryParse(txtStorageCapacity.Text, out int capacity))
                {
                    MessageBox.Show("Zadej platnou číselnou hodnotu pro kapacitu!");
                    return;
                }

                var newStorage = new Storage
                {
                    Storage_Location = txtStorageLocation.Text,
                    Storage_Capacity = capacity,
                    Last_Updated = DateTime.Now
                };

                int id = storageService.CreateStorage(newStorage);
                MessageBox.Show($"Sklad přidán! ID: {id}");

                RefreshStorageList();
                ClearStorageForm();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        private void btnChooseStorage_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

            RefreshStorageList();
        }

        private void btnUserUI_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();

            RefreshStorageList();
        }

        private void RefreshStorageList()
        {
            try
            {
                var storages = storageService.GetAllStorages();
                lstStorage.Items.Clear();

                foreach (var storage in storages)
                {
                    lstStorage.Items.Add(
                        $"{storage.Storage_ID} - {storage.Storage_Location} | Kapacita: {storage.Storage_Capacity}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání skladů: {ex.Message}");
            }
        }

        private void ClearStorageForm()
        {
            txtStorageLocation.Clear();
            txtStorageCapacity.Clear();
        }
    }
}