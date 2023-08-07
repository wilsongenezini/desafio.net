using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Extensoes;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    /// <summary>
    /// Atua como intermediário entre o modelo e a visualização.
    /// </summary>
    [Route("[Controller]")]
    public class CnabController : Controller
    {
        private readonly ILogger<CnabController> _logger;
        private readonly ICnabServico _invocaServico;

        /// <summary>
        /// Método construtor da classe CnabController
        /// </summary>
        /// <param name="logger">Registrar informações de log.</param>
        /// <param name="invocaServicos">Possibilidade de chamar os métodos dessa interface.</param>
        public CnabController(ILogger<CnabController> logger, ICnabServico invocaServicos)
        {
            _logger = logger;
            _invocaServico = invocaServicos;
        }

        /// <summary>
        /// Assinatura do método de ação Index.
        /// </summary>
        /// <returns>Retornar uma view para procurar por ela na pasta de Views.</returns>
        [HttpGet()]
        public IActionResult Index()
        {
            return View("ImportacaoCnab");
        }

        /// <summary>
        /// Método para tratar os dados do arquivo.
        /// </summary>
        /// <param name="arquivo">Este é o arquivo que será upado na página web.</param>
        /// <returns></returns>
        [HttpPost("importar")]
        public async Task<IActionResult> ImportarCnabAsync(IFormFile arquivo)
        {
            try
            {
                if (arquivo == null || arquivo.Length == 0)
                    return BadRequest("Nenhum arquivo foi enviado.");

                if (Path.GetExtension(arquivo.FileName).ToLower() != ".txt")
                    return BadRequest("Por favor, selecione um arquivo .txt válido.");

                var arquivoByte = arquivo.ToByteArray();

                await _invocaServico.ProcessarCnabAsync(arquivoByte);

                return Ok();
            }
            catch (Exception ex)
            {
                var erro = $"Ocorreu um erro ao enviar o arquivo: {ex.Message}";

                _logger.LogError(erro, ex);

                return StatusCode(500, erro);
            }
        }
    }
}