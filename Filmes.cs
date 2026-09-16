public class Filme
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Ano { get; set; }

    public Filme()
    {
    }

    public Filme(string titulo, string genero, int ano)
    {
        Titulo = titulo;
        Genero = genero;
        Ano = ano;
    }

    public override string ToString()
    {
        return $"{Id} - {Titulo} - {Genero} - {Ano}";
    }

    public void IsLongo()
    {
        if (Titulo.Length > 20)
        {
            Console.WriteLine("O título do filme é longo.");
        }
        else
        {
            Console.WriteLine("O título do filme não é longo.");
        }
    }
}



