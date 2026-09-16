public class Filme
{
    private string titulo;
    public string Titulo
    {
        get
        {
            return titulo;
        }
        set
        {
            titulo = value;
        }
    }
    private string genero;
    public string Genero
    {
        get
        {
            return genero;
        }
        set
        {
            genero = value;
        }
    }
    private DateTime ano;
    public DateTime Ano
    {
        get
        {
            return ano;
        }
        set
        {
            ano = value;
        }
    }
    public Filme(string titulo, string genero, DateTime ano)
    {
        this.titulo = titulo;
        this.genero = genero;
        this.ano = ano;
    }
    public override string ToString()
    {
    return $"Título: {this.titulo} | Genero: {this.genero} | Ano: {this.ano}";
    }
    }