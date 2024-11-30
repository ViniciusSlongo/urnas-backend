using Microsoft.AspNetCore.Mvc;
using Models.Configuracao;
using Models.Resultados;
using Services;
using ViewModel.Resultados;
namespace Controllers
{
    [ApiController]
    [Route("api/eleicao")]
    public class EleicaoController : ControllerBase
    {
        private readonly EleicaoService _eleicaoService;
        private readonly ResultadosEleicaoService _resultadosEleicaoService;

        public EleicaoController(EleicaoService eleicaoService, ResultadosEleicaoService resultadosEleicaoService)
        {
            _eleicaoService = eleicaoService;
            _resultadosEleicaoService = resultadosEleicaoService;
        }

        [HttpPost("importacoes-secoes")]
        public IActionResult ImportarResultados([FromBody] ResultadosEleicao resultado)
        {
            try
            {
                _resultadosEleicaoService.ConfigurarResultadosEleicao(resultado);
                return Ok(new { message = "Configuração de resultados importada com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
        
        [ProducesResponseType(typeof(StatusSecoesViewModel), 200)]
        [HttpGet("importacoes-secoes")]
        public IActionResult ObterStatusSecoes([FromQuery] string? zonaId, [FromQuery] string? secaoId)
        {
            try
            {
                StatusSecoesViewModel status = _resultadosEleicaoService.ObterStatusSecoes(zonaId, secaoId);
                return Ok(status);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
        [HttpPost()]
        public IActionResult ConfigurarEleicao([FromBody] Eleicao eleicao)
        {
            try
            {
                _eleicaoService.ConfigurarEleicao(eleicao);
                return Ok(new { message = "Configuração da eleição importada com sucesso." });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }


        [ProducesResponseType(typeof(ResultadosViewModel), 200)]
        [HttpGet("resultados")]
        public IActionResult ConsultarResultados([FromQuery] string? zonaId, [FromQuery] string? secaoId)
        {
            try
            {
                ResultadosViewModel resultadosViewModel = _resultadosEleicaoService.ObterResultadosConsolidados(zonaId, secaoId);

                return Ok(resultadosViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [ProducesResponseType(typeof(Eleicao), 200)]
        [HttpGet("")]
        public IActionResult ConsultarEleicao()
        {
            try {
                List<Eleicao> eleicaViewModel = _eleicaoService.ObterEleicao();
                return Ok(eleicaViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
