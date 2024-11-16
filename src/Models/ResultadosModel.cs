namespace Models.Resultados
{
    public class ResultadosEleicao
    {
        public Guid id { get; private set; }
        public string idZonaEleitoral { get; set; }
        public string idSecao { get; set; }
        public int quantidadePresentes { get; set; }
        public int votosValidos { get; set; }
        public List<CandidatoResultado> candidatos { get; set; }
    
        public ResultadosEleicao()
        {
            id = Guid.NewGuid();
            candidatos = new List<CandidatoResultado>();
        }

        public ResultadosEleicao(string idSecao, string idZonaEleitoral, int quantidadePresentes, int votosValidos, List<CandidatoResultado> candidatos) 
        {
 
            this.idSecao = idSecao ?? throw new ArgumentNullException(nameof(idSecao));
            this.idZonaEleitoral = idZonaEleitoral ?? throw new ArgumentNullException(nameof(idZonaEleitoral));
            this.quantidadePresentes = quantidadePresentes;
            this.votosValidos = votosValidos;
            this.candidatos = candidatos ?? throw new ArgumentNullException(nameof(candidatos));
        }
    }

    public class CandidatoResultado
    {
        public Guid id { get; private set; }
        public string nomeCandidato { get; set; }
        public int quantidadeVotos { get; set; }

        public CandidatoResultado()
        {
            id = Guid.NewGuid();  
        }

        public CandidatoResultado(string nomeCandidato, int quantidadeVotos) 
        {

            this.nomeCandidato = nomeCandidato ?? throw new ArgumentNullException(nameof(nomeCandidato));
            this.quantidadeVotos = quantidadeVotos;
        }
    }
}
