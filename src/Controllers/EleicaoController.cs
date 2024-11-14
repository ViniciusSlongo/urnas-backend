using Microsoft.AspNetCore.Mvc;
using Models.Configuracao;
using Services;

namespace Controllers
{
    [Route("api/eleicao")]
    [ApiController]
    public class EleicaoController : ControllerBase
    {
        private readonly EleicaoService _eleicaoService;

        public EleicaoController(EleicaoService eleicaoService)
        {
            _eleicaoService = eleicaoService;
        }

        [HttpPost]
        public IActionResult ConfigurarEleicao([FromBody] Eleicao eleicao)
        {
            try
            {
                _eleicaoService.ConfigurarEleicao(eleicao);
                return Ok(new { message = "Configuração importada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
