using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Configuracao
{
    public class Eleicao
    {
        public Guid id { get; set; }
        public string nomeEleicao { get; set; }
        public List<Candidato> candidatos { get; set; }
        public List<ZonaEleitoral> zonasEleitorais { get; set; }

        public Eleicao()
        {
            id = Guid.NewGuid(); 
            candidatos = new List<Candidato>();
            zonasEleitorais = new List<ZonaEleitoral>();
        }

        public Eleicao(string nomeEleicao, List<Candidato> candidatos, List<ZonaEleitoral> zonasEleitorais)
        {
            this.nomeEleicao = nomeEleicao ?? throw new ArgumentNullException(nameof(nomeEleicao));
            this.candidatos = candidatos ?? throw new ArgumentNullException(nameof(candidatos));
            this.zonasEleitorais = zonasEleitorais ?? throw new ArgumentNullException(nameof(zonasEleitorais));
        }
    }

    public class Candidato
    {
        public Guid id { get; set; }
        public string nome { get; set; }

        public Candidato()
        {
            id = Guid.NewGuid(); 
        }

        public Candidato(string nome)
        {
            this.nome = nome ?? throw new ArgumentNullException(nameof(nome));
        }
    }

    public class ZonaEleitoral
    {
        public string id { get; set; }
        public List<Secao> secoes { get; set; }

        public ZonaEleitoral()
        {
            secoes = new List<Secao>();
        }

        public ZonaEleitoral(string id, List<Secao> secoes)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
            this.secoes = secoes ?? throw new ArgumentNullException(nameof(secoes));

            foreach (var secao in secoes)
            {
                secao.ZonaEleitoralId = id;  
            }
        }
    }

    public class Secao
    {
        public string id { get; set; }
        
        public string? ZonaEleitoralId { get; set; }  

        public int quantidadeEleitores { get; set; }

        public Secao() { }

        public Secao(string id, int quantidadeEleitores)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
            this.quantidadeEleitores = quantidadeEleitores;
        }
    }
}
