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
            Console.WriteLine("=== SISTEMA CRUD DE USUÁRIOS ===");
            Console.WriteLine("1 - Cadastrar Novo Usuário");
            Console.WriteLine("2 - Listar Todos os Usuários");
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
    }}