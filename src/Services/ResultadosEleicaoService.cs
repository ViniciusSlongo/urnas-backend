using ViewModel.Resultados;
using Models.Resultados;
using Data;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;

namespace Services
{
    public class ResultadosEleicaoService
    {
        private readonly EleicaoContext _context;

        public ResultadosEleicaoService(EleicaoContext context)
        {
            _context = context;
        }

        public void AdicionarResultadosEleicao(ResultadosEleicao resultados)
        {
            _context.ResultadosEleicao.Add(resultados);
            _context.SaveChanges();
        }

        public void ConfigurarResultadosEleicao(ResultadosEleicao resultados)
        {
            if (resultados == null)
            {
                throw new ArgumentException("Os dados dos resultados da eleição estão vazios.");
            }

            if (string.IsNullOrWhiteSpace(resultados.idZonaEleitoral))
            {
                throw new ArgumentException("ID da Zona Eleitoral é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(resultados.idSecao))
            {
                throw new ArgumentException("ID da seção é obrigatório.");
            }

            if (resultados.quantidadePresentes < 0)
            {
                throw new ArgumentException("A quantidade de presentes não pode ser negativa.");
            }

            if (resultados.votosValidos < 0 || resultados.votosValidos > resultados.quantidadePresentes)
            {
                throw new ArgumentException("Os votos válidos devem ser não negativos e não podem exceder a quantidade de presentes.");
            }

            if (resultados.candidatos == null || !resultados.candidatos.Any())
            {
                throw new ArgumentException("É necessário informar pelo menos um candidato com seus resultados.");
            }

            foreach (var candidato in resultados.candidatos)
            {
                if (string.IsNullOrWhiteSpace(candidato.nomeCandidato))
                {
                    throw new ArgumentException("Nome do candidato é obrigatório.");
                }

                if (candidato.quantidadeVotos < 0)
                {
                    throw new ArgumentException($"A quantidade de votos para o candidato '{candidato.nomeCandidato}' deve ser maior ou igual a zero.");
                }
            }

            var zonaEleitoralDosResultados = _context.ZonaEleitoral.FirstOrDefault(x => x.id == resultados.idZonaEleitoral) ?? throw new ArgumentException("Zona Eleitoral não encontrada!");

            var secaoDosResultados = _context.Secao.FirstOrDefault(x => x.id == resultados.idSecao) ?? throw new ArgumentException("Seção não encontrada!");

            if (resultados.quantidadePresentes > secaoDosResultados.quantidadeEleitores)
            {
                throw new ArgumentException("A quantidade de presentes não pode ser mais que a quantidade de eleitores da seção.");
            }

            var somaVotosCandidatos = resultados.candidatos.Sum(c => c.quantidadeVotos);
            if (somaVotosCandidatos > resultados.votosValidos)
            {
                throw new ArgumentException("A soma dos votos dos candidatos não pode ser maior que a quantidade de votos válidos da seção.");
            }

            AdicionarResultadosEleicao(resultados);
        }

        public ResultadosViewModel ObterResultadosConsolidados(string? zonaId, string? secaoId)
        {
            var resultadosFiltrados = _context.ResultadosEleicao.AsQueryable();

            if (!string.IsNullOrWhiteSpace(secaoId))
            {
                resultadosFiltrados = resultadosFiltrados
                    .Where(r => r.idSecao.StartsWith(secaoId));
            }

            if (!string.IsNullOrWhiteSpace(zonaId))
            {
                resultadosFiltrados = resultadosFiltrados
                    .Where(r => r.idZonaEleitoral == zonaId);
            }

            var totalPresentes = resultadosFiltrados.Sum(r => r.quantidadePresentes);
            var totalVotosValidos = resultadosFiltrados.Sum(r => r.votosValidos);

            var candidatosConsolidados = resultadosFiltrados
                .SelectMany(r => r.candidatos)
                .GroupBy(c => c.nomeCandidato)
                .Select(g => new CandidatoResultadosViewModel
                {
                    nomeCandidato = g.Key,
                    quantidadeVotos = g.Sum(c => c.quantidadeVotos),
                    percentualVotos = totalVotosValidos > 0
                        ? (float)g.Sum(c => c.quantidadeVotos) / totalVotosValidos * 100
                        : 0
                })
                .ToList();

            return new ResultadosViewModel
            {
                totalVotosValidos = totalVotosValidos,
                percentualVotosValidos = totalPresentes > 0
                    ? (float)totalVotosValidos / totalPresentes * 100
                    : 0,
                candidatos = candidatosConsolidados
            };
        }

        public StatusSecoesViewModel ObterStatusSecoes(string? zonaId, string? secaoId)
        {
            var secoesFiltradas = _context.Secao.AsQueryable();
            var resultadosFiltrados = _context.ResultadosEleicao.AsQueryable();

            if (!string.IsNullOrWhiteSpace(secaoId))
            {
                secoesFiltradas = secoesFiltradas.Where(r => r.id == secaoId) ?? throw new ArgumentException("Seção não encontrada!");
                
                resultadosFiltrados = resultadosFiltrados
                    .Where(r => r.idSecao == secaoId); 
            }

            if (!string.IsNullOrWhiteSpace(zonaId))
            {
                secoesFiltradas = secoesFiltradas.Where(r => r.ZonaEleitoralId == zonaId) ?? throw new ArgumentException("Zona Eleitoral não encontrada!");

                resultadosFiltrados = resultadosFiltrados
                    .Where(r => r.idZonaEleitoral == zonaId);
            }

            
            int secoesImportadas = resultadosFiltrados
                .Select(r => r.idSecao)
                .Distinct()
                .Count(); 

            int totalSecoes = secoesFiltradas
                .Select(r => r.id)
                .Distinct()
                .Count(); 

            int totalEleitoresPresentes = resultadosFiltrados.Sum(r => r.quantidadePresentes);
            int totalEleitores = secoesFiltradas.Sum(s => s.quantidadeEleitores);

            int totalAbstencoes = 0;
            if (secoesImportadas > 0)
            {
                totalAbstencoes = totalEleitores - totalEleitoresPresentes;
            }

            float percentualAbstencoes = totalEleitores > 0 ? totalAbstencoes * 100 / totalEleitores : 0;
            float percentualPresentes = totalEleitores > 0 ? totalEleitoresPresentes * 100 / totalEleitores : 0;
      
            return new StatusSecoesViewModel
            {
                TotalSecoes = totalSecoes,
                SecoesImportadas = secoesImportadas, 
                TotalEleitoresPresentes = totalEleitoresPresentes,
                PercentualPresentes = percentualPresentes,
                TotalAbstencoes = totalAbstencoes,
                PercentualAbstencoes = percentualAbstencoes
            };
        }

    }
}
