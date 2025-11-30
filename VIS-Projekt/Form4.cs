namespace VIS_Projekt
{
    public partial class Form4 : Form
    {
        public string UserRole { get; private set; } = string.Empty;

        public Form4()
        {
            InitializeComponent();

            txtPassword.PasswordChar = '*';

            btnLogin.Click += btnLogin_Click;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "heslo")
            {
                UserRole = "admin";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (username == "user" && password == "heslo")
            {
                UserRole = "user";
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