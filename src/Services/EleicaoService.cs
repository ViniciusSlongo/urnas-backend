using Models.Configuracao;
using Data;

namespace Services
{
    public class EleicaoService
    {
        private readonly EleicaoContext _context;

        public EleicaoService(EleicaoContext context)
        {
            _context = context;
        }

        public void AdicionarEleicao(Eleicao eleicao)
        {
            _context.Eleicoes.Add(eleicao); 
            _context.SaveChanges();
        }
        public void ConfigurarEleicao(Eleicao eleicao)
        {
            if (eleicao == null)
            {
                throw new ArgumentException("O JSON de configuração está vazio.");
            }

            if (string.IsNullOrWhiteSpace(eleicao.NomeEleicao))
            {
                throw new ArgumentException("Nome da eleição é obrigatório.");
            }

            if (eleicao.Candidatos == null || !eleicao.Candidatos.Any())
            {
                throw new ArgumentException("É necessário informar pelo menos um candidato.");
            }
            
            foreach (var zona in eleicao.ZonasEleitorais)
            {
                if (string.IsNullOrWhiteSpace(zona.Id))
                {
                    throw new ArgumentException("ID da zona eleitoral é obrigatório.");
                }
                
                foreach (var secao in zona.Secoes)
                {
                    if (string.IsNullOrWhiteSpace(secao.Id))
                    {
                        throw new ArgumentException("ID da seção é obrigatório.");
                    }
                    if (secao.QuantidadeEleitores <= 0)
                    {
                        throw new ArgumentException("A quantidade de eleitores na seção deve ser maior que zero.");
                    }
                }
            }

            AdicionarEleicao(eleicao);

        }
    }
}
