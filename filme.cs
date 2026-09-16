public class Filme
{
    private int id;
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
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
    private DateOnly ano;
    public DateOnly Ano
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
    public Filme(int id, string titulo, string genero, DateOnly ano)
    {
        this.id = id;
        this.titulo = titulo;
        this.genero = genero;
        this.ano = ano;
    }
    public override string ToString()
    {
    return $"Id: {this.id} | Título: {this.titulo} | Genero: {this.genero} | Ano: {this.ano}";
    }
    }