using MySql.Data.MySqlClient;

class Program
{
    

    static void Main(string[] args)
    {

        string connectionString = "Server=localhost;Database=ronaldo;Uid=root;Pwd=Senac2026;";

        using (MySqlConnection conexao = new MySqlConnection(connectionString))
        {
            try
            {
                Console.WriteLine("Conectando ao banco de dados...");
                conexao.Open();
                Console.WriteLine("Conexão realizada com sucesso!\n");

                string sql = "Comando do sql aqui";
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar: {ex.Message}");
            }
        }
    }
};