namespace Models.Configuracao
{
    public class Eleicao
    {
        public int Id { get; set; }
        public string NomeEleicao { get; set; }
        public List<Candidato> Candidatos { get; set; }
        public List<ZonaEleitoral> ZonasEleitorais { get; set; }

        // Construtor sem parâmetros para o Entity Framework
        public Eleicao()
        {
            Candidatos = new List<Candidato>();
            ZonasEleitorais = new List<ZonaEleitoral>();
        }

        // Construtor com parâmetros, caso você queira usá-lo em outras partes da aplicação
        public Eleicao(int id, string nomeEleicao, List<Candidato> candidatos, List<ZonaEleitoral> zonasEleitorais)
        {
            Id = id;
            NomeEleicao = nomeEleicao ?? throw new ArgumentNullException(nameof(nomeEleicao));
            Candidatos = candidatos ?? throw new ArgumentNullException(nameof(candidatos));
            ZonasEleitorais = zonasEleitorais ?? throw new ArgumentNullException(nameof(zonasEleitorais));
        }
    }

    public class Candidato
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        // Construtor sem parâmetros para o Entity Framework
        public Candidato()
        {
        }

        // Construtor com parâmetros, caso você queira usá-lo em outras partes da aplicação
        public Candidato(string id, string nome)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
    }

    public class ZonaEleitoral
    {
        public string Id { get; set; }
        public List<Secao> Secoes { get; set; }

        // Construtor sem parâmetros para o Entity Framework
        public ZonaEleitoral()
        {
            Secoes = new List<Secao>();
        }

        // Construtor com parâmetros, caso você queira usá-lo em outras partes da aplicação
        public ZonaEleitoral(string id, List<Secao> secoes)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Secoes = secoes ?? throw new ArgumentNullException(nameof(secoes));
        }
    }

    public class Secao
    {
        public string Id { get; set; }
        public int QuantidadeEleitores { get; set; }

        // Construtor sem parâmetros para o Entity Framework
        public Secao()
        {
        }

        // Construtor com parâmetros, caso você queira usá-lo em outras partes da aplicação
        public Secao(string id, int quantidadeEleitores)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            QuantidadeEleitores = quantidadeEleitores;
        }
    }
}
