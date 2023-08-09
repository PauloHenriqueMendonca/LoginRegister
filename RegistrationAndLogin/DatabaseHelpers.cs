using System.Data.SQLite;
using System.IO;
using System.Web;

namespace LoginRegister
{
    public static class DatabaseHelpers
    {
        private static string ServerPath = HttpContext.Current.Server.MapPath("~/DBFiles");
        private static string connectionString = $"Data Source={ServerPath}/DBTest.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists($"{ServerPath}/DBTest.db"))
            {
                //creates the database file
                SQLiteConnection.CreateFile($"{ServerPath}/DBTest.db");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    
                    //Create tables for your data
                    string createUsersTableQuery = @"
                        CREATE TABLE IF NOT EXISTS users(
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            name TEXT NOT NULL,
                            email TEXT UNIQUE NOT NULL,
                            password TEXT NOT NULL
                    );";

                    using (var command =new SQLiteCommand(connection))
                    {
                        command.CommandText = createUsersTableQuery;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}