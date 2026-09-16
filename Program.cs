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
            Console.WriteLine("3 - Atualizar filme");
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
        DateOnly exAno = DateOnly.Parse(Console.ReadLine());

        Filme novoFilme = new Filme(0, exTitulo, exGenero, exAno);

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
    }
    static void Listar()
    {
        Console.Clear();
        Console.WriteLine("--- LISTA DE FILMES ---");

        using (MySqlConnection conexao = new MySqlConnection(connectionString))
        {
            try
            {
                conexao.Open();
                string sql = "SELECT id, titulo, genero, ano FROM filme";

                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    using (MySqlDataReader leitor = comando.ExecuteReader())
                    {
                        if (!leitor.HasRows)
                        {
                            Console.WriteLine("Nenhum filme encontrado.");
                        }
                        else
                        {
                            while (leitor.Read())
                            {
                                int idBanco = Convert.ToInt32(leitor["id"]);
                                string t = leitor["titulo"].ToString();
                                string g = leitor["genero"].ToString();
                                DateTime dataBanco = Convert.ToDateTime(leitor["ano"]);
                                DateOnly a = DateOnly.FromDateTime(dataBanco);
                                Filme f = new Filme(idBanco, t, g, a);
                                Console.WriteLine(f.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao listar: {ex.Message}");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
    static void Atualizar()
{
    Console.Clear();
    Console.WriteLine("--- ATUALIZAR FILME PELO ID (UPDATE) ---");

    
    Console.Write("Digite o ID do filme que deseja atualizar: ");
    int idAlvo = Convert.ToInt32(Console.ReadLine());

    Console.Write("Digite o NOVO Titulo: ");
    string? novoTitulo = Console.ReadLine();

    Console.Write("Digite o NOVO Gênero: ");
    string? novoGenero = Console.ReadLine();

    Console.Write("Digite a NOVA Data de Lançamento (DD/MM/AAAA): ");
    DateOnly novoAno = DateOnly.Parse(Console.ReadLine());

    using (MySqlConnection conexao = new MySqlConnection(connectionString))
    {
        try
        {
            conexao.Open();
            
            string sql = "UPDATE filme SET titulo = @titulo, genero = @genero, ano = @ano WHERE id = @id";

            using (MySqlCommand comando = new MySqlCommand(sql, conexao))
            {
                comando.Parameters.AddWithValue("@titulo", novoTitulo);
                comando.Parameters.AddWithValue("@genero", novoGenero);
                comando.Parameters.AddWithValue("@ano", novoAno.ToDateTime(TimeOnly.MinValue));
                comando.Parameters.AddWithValue("@id", idAlvo);

                int linhasAfetadas = comando.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"\nSucesso! O filme de ID {idAlvo} foi atualizado.");
                }
                else
                {
                    Console.WriteLine("\nNenhum filme encontrado com esse ID.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao atualizar: {ex.Message}");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
}
static void Deletar()
{
    Console.Clear();
    Console.WriteLine("--- DELETAR FILME PELO ID (DELETE) ---");

    Console.Write("Digite o ID do filme que deseja apagar permanentemente: ");
    int idAlvo = Convert.ToInt32(Console.ReadLine());

    using (MySqlConnection conexao = new MySqlConnection(connectionString))
    {
        try
        {
            conexao.Open();
            
            string sql = "DELETE FROM filme WHERE id = @id";

            using (MySqlCommand comando = new MySqlCommand(sql, conexao))
            {
                comando.Parameters.AddWithValue("@id", idAlvo);

                int linhasAfetadas = comando.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine($"\nSucesso! O filme com ID {idAlvo} foi excluído do banco de dados.");
                }
                else
                {
                    Console.WriteLine("\nNenhum filme foi encontrado com esse ID.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao deletar: {ex.Message}");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
}
}