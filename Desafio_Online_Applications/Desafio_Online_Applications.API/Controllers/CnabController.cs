﻿using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Extensoes;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    [Route("[Controller]")]
    public class CnabController : Controller
    {
        private readonly ILogger<CnabController> _logger;
        private readonly ICnabServicos _invocaServico;

        public CnabController(ILogger<CnabController> logger, ICnabServicos invocaServicos)
        {
            _logger = logger;
            _invocaServico = invocaServicos;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View("ImportacaoCnab");
        }

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
                return StatusCode(400, $"Ocorreu um erro ao enviar o arquivo: {ex.Message}");
            }
        }
    }
}