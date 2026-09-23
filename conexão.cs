using MySql.Data.MySqlClient;

public class Conexao
{
    private string stringConexao = "Server=localhost;Database=sistema_filmes;Uid=root;Pwd=;"; // serve para saber onde está o o banco e como entrar nele

    public MySqlConnection Conectar()
    {
        MySqlConnection conexao = new MySqlConnection(stringConexao);

        return conexao;
    }
}