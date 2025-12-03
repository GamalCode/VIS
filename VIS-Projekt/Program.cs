using DataAccess.GlobalConfig;
using Domain.Session;
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

                if (result == DialogResult.OK && UserSession.IsLoggedIn)
                {
                    if (UserSession.IsAdmin())
                    {
                        Application.Run(new Form1());
                    }
                    else if (UserSession.IsUser())
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