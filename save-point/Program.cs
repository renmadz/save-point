using Microsoft.EntityFrameworkCore;
using save_point.Data;

namespace save_point
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Create the database on first launch and apply any pending
            // migrations on later launches, before any UI needs it.
            try
            {
                using var db = new SavePointContext();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to initialize the database:\n\n{ex.Message}",
                    "Save Point",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new SavePoint());
        }
    }
}
