using Desafio_Online_Applications.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    public class OperacoesController : Controller
    {
        private readonly IOperacoesServico _operacoesServicos;

        public OperacoesController(IOperacoesServico operacoesServicos)
        {
            _operacoesServicos = operacoesServicos;
        }

        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = await _operacoesServicos.VisualizarListaOperacoesAsync();
            
            return View("ListagemOperacoes", viewModel);
        }
    }
}
