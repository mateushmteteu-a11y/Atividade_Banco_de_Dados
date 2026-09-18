using MySql.Data.MySqlClient;

Conexao conexao = new Conexao();

int opcao = -1;

while (opcao != 0)
{
    Console.Clear();

    Console.WriteLine("===========================");
    Console.WriteLine("      SISTEMA DE FILMES");
    Console.WriteLine("===========================");
    Console.WriteLine("1 - Cadastrar filme");
    Console.WriteLine("2 - Listar filmes");
    Console.WriteLine("3 - Buscar filme");
    Console.WriteLine("4 - Atualizar filme");
    Console.WriteLine("5 - Excluir filme");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("===========================");
    Console.Write("Escolha uma opção: ");

    try
    {
        opcao = Convert.ToInt32(Console.ReadLine());

        // CADASTRAR
        if (opcao == 1)
        {
            Console.Write("Digite o título: ");
            string titulo = Console.ReadLine();


            Console.Write("Digite o gênero: ");
            string genero = Console.ReadLine();

            Console.Write("Digite o ano: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            MySqlConnection banco = conexao.Conectar();

            banco.Open();

            string sql = "INSERT INTO filmes (titulo, genero, ano) VALUES (@titulo, @genero, @ano)";

            MySqlCommand comando = new MySqlCommand(sql, banco);

            comando.Parameters.AddWithValue("@titulo", titulo);
            comando.Parameters.AddWithValue("@genero", genero);
            comando.Parameters.AddWithValue("@ano", ano);

            comando.ExecuteNonQuery();

            banco.Close();

            Console.WriteLine("Filme cadastrado com sucesso!");
        }

        // LISTAR
        else if (opcao == 2)
        {
            MySqlConnection banco = conexao.Conectar();

            banco.Open();

            string sql = "SELECT * FROM filmes";

            MySqlCommand comando = new MySqlCommand(sql, banco);

            MySqlDataReader leitor = comando.ExecuteReader();

            Console.WriteLine("\n===== FILMES =====");

            while (leitor.Read())
            {
                Filmes filme = new Filmes();

                filme.Id = Convert.ToInt32(leitor["id"]);
                filme.Titulo = leitor["titulo"].ToString();
                filme.Genero = leitor["genero"].ToString();
                filme.Ano = Convert.ToInt32(leitor["ano"]);

                Console.WriteLine(filme);
            }

            banco.Close();
        }

        // BUSCAR
        else if (opcao == 3)
        {
            Console.Write("Digite o ID do filme: ");
            int id = Convert.ToInt32(Console.ReadLine());

            MySqlConnection banco = conexao.Conectar();

            banco.Open();

            string sql = "SELECT * FROM filmes WHERE id = @id";

            MySqlCommand comando = new MySqlCommand(sql, banco);

            comando.Parameters.AddWithValue("@id", id);

            MySqlDataReader leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                Filmes filme = new Filmes();

                filme.Id = Convert.ToInt32(leitor["id"]);
                filme.Titulo = leitor["titulo"].ToString();
                filme.Genero = leitor["genero"].ToString();
                filme.Ano = Convert.ToInt32(leitor["ano"]);

                Console.WriteLine("\nFilme encontrado:");
                Console.WriteLine(filme);
            }
            else
            {
                Console.WriteLine("Filme não encontrado.");
            }

            banco.Close();
        }

        // ATUALIZAR
        else if (opcao == 4)
        {
            Console.Write("Digite o ID do filme: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o novo título: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o novo gênero: ");
            string genero = Console.ReadLine();

            Console.Write("Digite o novo ano: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            MySqlConnection banco = conexao.Conectar();

            banco.Open();

            string sql = "UPDATE filmes SET titulo = @titulo, genero = @genero, ano = @ano WHERE id = @id";

            MySqlCommand comando = new MySqlCommand(sql, banco);

            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@titulo", titulo);
            comando.Parameters.AddWithValue("@genero", genero);
            comando.Parameters.AddWithValue("@ano", ano);

            int resultado = comando.ExecuteNonQuery();

            banco.Close();

            if (resultado > 0)
            {
                Console.WriteLine("Filme atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Filme não encontrado.");
            }
        }

        // EXCLUIR
        else if (opcao == 5)
        {
            Console.Write("Digite o ID do filme: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Tem certeza que deseja excluir? (s/n): ");
            string confirmacao = Console.ReadLine();

            if (confirmacao.ToLower() == "s")
            {
                MySqlConnection banco = conexao.Conectar();

                banco.Open();

                string sql = "DELETE FROM filmes WHERE id = @id";

                MySqlCommand comando = new MySqlCommand(sql, banco);

                comando.Parameters.AddWithValue("@id", id);

                int resultado = comando.ExecuteNonQuery();

                banco.Close();

                if (resultado > 0)
                {
                    Console.WriteLine("Filme excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Filme não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Exclusão cancelada.");
            }
        }

        // SAIR
        else if (opcao == 0)
        {
            Console.WriteLine("Programa encerrado.");
        }

        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }
    catch
    {
        Console.WriteLine("Digite um valor válido.");
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }
}


