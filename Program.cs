using MySql.Data.MySqlClient;

class Program
{
    static string connectionString = "Server=localhost;Database=ronaldo;Uid=root;Pwd=Senac2026;";

    static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();//limpa as coisas eu pessoalmento amei
            Console.WriteLine("=== SISTEMA CRUD DE FILME ===");
            Console.WriteLine("1 - Cadastrar filme");
            Console.WriteLine("2 - Listar Todos os filmes");
            Console.WriteLine("3 - Atualizar filme");
            Console.WriteLine("4 - Deletar filme");
            Console.WriteLine("5 - Buscar por nome do filme");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine() ?? "";

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
                case "5":
                    BuscarPorNome();
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("\nSaindo do sistema... Até logo!");
                    break;
                default:
                    Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();//tem que aperta uma tecla pro o sistema continua gostei tambem
                    break;
            }
        }
    }
    static void Cadastrar()
    {
        Console.Clear();
        Console.WriteLine("--- NOVO CADASTRO ---");

        string? exTitulo;
        while (true)
        {
        Console.Write("Título do Filme: ");
        exTitulo = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(exTitulo))
        {
            break; 
        }
        Console.WriteLine(" O título do filme não pode ficar em branco! Tente novamente.");
        }

        
        string? exGenero;
        while (true)
        {
        Console.Write("Gênero: ");
        exGenero = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(exGenero))
        {
            break;
        }
        Console.WriteLine("O gênero do filme não pode ficar em branco! Tente novamente.");
        }


        Console.Write("Data de Lançamento (DD/MM/AAAA): ");
        DateOnly exAno = DateOnly.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd/MM/aaaa"));

        Filme novoFilme = new Filme(0, exTitulo, exGenero, exAno);

        using (MySqlConnection conexao = new MySqlConnection(connectionString))//cria conexão
        {
            try
            {
                conexao.Open();
                string sql = "INSERT INTO filme (titulo, genero, ano) VALUES (@titulo, @genero, @ano)";//cria o camando para ser usado no banco
                
                using (MySqlCommand comando = new MySqlCommand(sql, conexao))//criar algun como uma classe so que com o comando do mysql
                {
                    comando.Parameters.AddWithValue("@titulo", novoFilme.Titulo);//atribui os valores ao comando
                    comando.Parameters.AddWithValue("@genero", novoFilme.Genero);
                    comando.Parameters.AddWithValue("@ano", novoFilme.Ano.ToDateTime(TimeOnly.MinValue));

                    comando.ExecuteNonQuery();//envia e executa o comando no mysql
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
                                string t = leitor["titulo"].ToString() ?? "";
                                string g = leitor["genero"].ToString() ?? "";
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

    Console.Clear();
        Console.WriteLine("--- NOVO CADASTRO ---");

        string? novoTitulo;
        while (true)
        {
        Console.Write("Título do Filme: ");
        novoTitulo = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(novoTitulo))
        {
            break; 
        }
        Console.WriteLine(" O título do filme não pode ser vazio.");
        }

        
        string? novoGenero;
        while (true)
        {
        Console.Write("Gênero: ");
        novoGenero = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(novoGenero))
        {
            break;
        }
        Console.WriteLine("O gênero do filme não pode ser vazio.");
        }


        Console.Write("Data de Lançamento (DD/MM/AAAA): ");
        DateOnly novoAno = DateOnly.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd/MM/aaaa"));


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
    Console.Write("Tem certeza?(S/N) ");
    string esco = Console.ReadLine()?.ToUpper() ?? "";
    if (esco == "S")
        {
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
    else if (esco == "N")
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    else
        {
            Console.WriteLine("\nOpção invalida, Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
}
static void BuscarPorNome()
{
    Console.Clear();
    Console.WriteLine("--- BUSCAR FILME POR NOME ---");

    Console.Write("Digite o nome (ou parte do nome) do filme: ");
    string pesquisa = Console.ReadLine() ?? "";

    using (MySqlConnection conexao = new MySqlConnection(connectionString))
    {
        try
        {
            conexao.Open();
            // O LIKE permite buscar termos parciais "precisa tar entre %" (ex: "Minecraft" acha "Um filme Minecraft")
            string sql = "SELECT id, titulo, genero, ano FROM filme WHERE titulo LIKE @pesquisa";

            using (MySqlCommand comando = new MySqlCommand(sql, conexao))
            {
                // O "%" + pesquisa + "%" para combrar com o LIKE
                comando.Parameters.AddWithValue("@pesquisa", "%" + pesquisa + "%");

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (!leitor.HasRows)
                    {
                        Console.WriteLine("\nNenhum filme encontrado com esse termo.");
                    }
                    else
                    {
                        Console.WriteLine("\n--- Filmes Encontrados ---");
                        while (leitor.Read())
                        {
                            int id = Convert.ToInt32(leitor["id"]);
                            string t = leitor["titulo"].ToString() ?? "";
                            string g = leitor["genero"].ToString() ?? "";
                            DateTime dataBanco = Convert.ToDateTime(leitor["ano"]);
                            DateOnly a = DateOnly.FromDateTime(dataBanco);

                            Filme f = new Filme(id, t, g, a);
                            Console.WriteLine(f.ToString());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao buscar: {ex.Message}");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
    Console.ReadKey();
}

}