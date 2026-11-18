
using MySqlConnector;

namespace Trabalho_PI.Database
{
    public class Conexao
    {
        private static readonly string connectionString =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=organizador;" +
            "User ID=root;" +
            "Password= Senac2026;";

        public static MySqlConnection Conectar()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
