using Desafio_Online_Applications.Core.Models;
using Desafio_Online_Applications.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    public class OperacoesController : Controller
    {
        private readonly IOperacoesServicos _operacoesServicos;

        public OperacoesController(IOperacoesServicos operacoesServicos)
        {
            _operacoesServicos = operacoesServicos;
        }

        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            OperacoesViewModel viewModel = await _operacoesServicos.VisualizarListaOperacoes();
            
            return View("ListagemOperacoes", viewModel);
        }
    }
}
