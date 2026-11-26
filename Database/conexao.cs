using MySql.Data.MySqlClient;

namespace trabalho_PI.Database
{
    public class Conexao
    {
        private string conexaoString =
            "Server=localhost;Port=3306;Database=organizador;Uid=root;Pwd=Senac2026;";

        public MySqlConnection Conectar()
        {
            return new MySqlConnection(conexaoString);
        }
    
        public void TestarConexao()
        {
        try
        {
            using (MySqlConnection conexao = Conectar())
            {
                conexao.Open();
                Console.WriteLine("Conexão com MySQL realizada com sucesso!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar: " + ex.Message);
        }
        }
    }
}
