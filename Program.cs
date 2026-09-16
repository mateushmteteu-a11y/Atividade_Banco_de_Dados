using System;
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
                Console.WriteLine("Tentando conectar ao MySQL...");
                
               
                conexao.Open();
                Console.WriteLine("Conexão realizada com sucesso! 🎉");

              
                string query = "SELECT VERSION();";
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    string versao = comando.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Versão do MySQL: {versao}");
                }
            }
            catch (MySqlException ex)
            {
              
                Console.WriteLine($"Erro de banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro geral: {ex.Message}");
            }
        }
    }
}
