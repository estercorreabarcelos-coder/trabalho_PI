using MySqlConnector;

public class Conexao
{
    private string conexaoString =
        "Server=localhost" +
        "Database=organizador" +
        "Uid=root" +
        "Pwd=Senac2026";

    public MySqlConnection Conectar()
    {
        return new MySqlConnection(conexaoString);
    }
}


