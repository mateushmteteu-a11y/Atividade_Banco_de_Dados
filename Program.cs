using MySql.Data.MySqlClient;

class Program
{
    static string connectionString = "Server=localhost;Database=ronaldo;Uid=root;Pwd=Senac2026;";

    static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA CRUD DE FILME ===");
            Console.WriteLine("1 - Cadastrar filme");
            Console.WriteLine("2 - Listar Todos os filmes");
            Console.WriteLine("3 - Atulizar filme");
            Console.WriteLine("4 - Deletar filme");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Cadastrar();
                    break;
                case "2":
                    Listar();
                    break;
                case "3":
                    Atualizar();
                    break;
                case "4":
                    Deletar();
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("\nSaindo do sistema... Até logo!");
                    break;
                default:
                    Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    static void Cadastrar()
    {
        Console.Clear();
        Console.WriteLine("--- NOVO CADASTRO ---");

        Console.Write("Título do Filme: ");
        string? exTitulo = Console.ReadLine();

        Console.Write("Gênero: ");
        string? exGenero = Console.ReadLine();

        Console.Write("Data de Lançamento (DD/MM/AAAA): ");
        DateTime exAno = DateTime.Parse(Console.ReadLine());

        Filme novoFilme = new Filme(exTitulo, exGenero, exAno);

        using (MySqlConnection conexao = new MySqlConnection(connectionString))
        {
            try
            {
                conexao.Open();
                string sql = "INSERT INTO filme (titulo, genero, ano) VALUES (@titulo, @genero, @ano)";
                
                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@titulo", novoFilme.Titulo);
                    comando.Parameters.AddWithValue("@genero", novoFilme.Genero);
                    comando.Parameters.AddWithValue("@ano", novoFilme.Ano);

                    comando.ExecuteNonQuery();
                    Console.WriteLine($"\n{novoFilme.Titulo} foi cadastrado no MySQL.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao salvar: {ex.Message}");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }}