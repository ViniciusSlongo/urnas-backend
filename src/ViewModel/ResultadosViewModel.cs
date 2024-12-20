using System.Runtime.Intrinsics.X86;
using Microsoft.Identity.Client;

namespace ViewModel.Resultados 
{
    public class ResultadosViewModel 
    {
        public int totalVotosValidos {get;set;}
        public float percentualVotosValidos {get;set;}
        public List<CandidatoResultadosViewModel> candidatos {get;set;}
    }

    public class CandidatoResultadosViewModel 
    {
        public string nomeCandidato {get;set;}
        public int quantidadeVotos {get;set;}
        public float percentualVotos {get;set;}
    }
    
    public class StatusSecoesViewModel
    {
        public int TotalSecoes { get; set; }
        public int SecoesImportadas { get; set; }
        public int TotalEleitoresPresentes { get; set; }
        public float PercentualPresentes { get; set; }
        public int TotalAbstencoes { get; set; }
        public float PercentualAbstencoes { get; set; }
    }

    public class AtualizarSecaoInputModel
    {
        public string IdSecao { get; set; }
        public int QuantidadePresentes { get; set; }
        public int VotosValidos { get; set; }
        public List<CandidatoInputModel> Candidatos { get; set; }
    }

    public class CandidatoInputModel
    {
        public string NomeCandidato { get; set; }
        public int QuantidadeVotos { get; set; }
    }
}