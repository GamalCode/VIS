using DataAccess.GlobalConfig;
using SQLitePCL;

namespace VIS_Projekt
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Batteries.Init();
                GlobalConfig.InitializeConnections(DatabaseType.Sql);

                ApplicationConfiguration.Initialize();

                Form4 loginForm = new Form4();
                DialogResult result = loginForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (loginForm.UserRole == "admin")
                    {
                        Application.Run(new Form1());
                    }
                    else if (loginForm.UserRole == "user")
                    {
                        Application.Run(new Form3());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CHYBA: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}");
            }
        }
    }
}