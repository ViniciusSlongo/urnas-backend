using Models.Configuracao;
using Data;
using Microsoft.EntityFrameworkCore;

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

            if (string.IsNullOrWhiteSpace(eleicao.nomeEleicao))
            {
                throw new ArgumentException("Nome da eleição é obrigatório.");
            }

            if (eleicao.candidatos == null || !eleicao.candidatos.Any())
            {
                throw new ArgumentException("É necessário informar pelo menos um candidato.");
            }
            
            foreach (var zona in eleicao.zonasEleitorais)
            {
                if (string.IsNullOrWhiteSpace(zona.id))
                {
                    throw new ArgumentException("ID da zona eleitoral é obrigatório.");
                }
                
                foreach (var secao in zona.secoes)
                {
                    if (string.IsNullOrWhiteSpace(secao.id))
                    {
                        throw new ArgumentException("ID da seção é obrigatório.");
                    }
                    if (secao.quantidadeEleitores <= 0)
                    {
                        throw new ArgumentException("A quantidade de eleitores na seção deve ser maior que zero.");
                    }
                }
            }

            AdicionarEleicao(eleicao);

        }

        public List<Eleicao> ObterEleicao() 
        {
            return _context.Eleicoes
                .Include(e => e.candidatos) 
                .Include(e => e.zonasEleitorais) 
                    .ThenInclude(z => z.secoes) 
                .ToList();
        }
    }
}
