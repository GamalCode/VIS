using Domain.Services;

namespace VIS_Projekt
{
    public partial class Form4 : Form
    {
        private readonly AuthService _authService;

        public Form4()
        {
            InitializeComponent();

            _authService = new AuthService();
            txtPassword.PasswordChar = '*';

            btnLogin.Click += btnLogin_Click;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vyplňte prosím jméno i heslo!",
                    "Chyba přihlášení",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool loginSuccessful = _authService.Login(username, password);

            if (loginSuccessful)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Špatné přihlašovací údaje! Zkuste to znovu.",
                    "Chyba přihlášení",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtName.Focus();
            }
        }
    }
}