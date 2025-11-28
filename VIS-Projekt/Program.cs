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
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CHYBA: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}");
            }
        }
    }
}