using Desafio_Online_Applications.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    /// <summary>
    /// Classe que trata as operações sem erros.
    /// </summary>
    public class OperacoesController : Controller
    {
        private readonly IOperacoesServico _operacoesServicos;

        /// <summary>
        /// Método construtor da classe OperacoesController.
        /// </summary>
        /// <param name="operacoesServicos">Para utilizar métodos da interface.</param>
        public OperacoesController(IOperacoesServico operacoesServicos)
        {
            _operacoesServicos = operacoesServicos;
        }

        /// <summary>
        /// Método que retorna para a view.
        /// </summary>
        /// <returns>Retorna para a view as operações sem erros.</returns>
        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = await _operacoesServicos.VisualizarListaOperacoesAsync();
            
            return View("ListagemOperacoes", viewModel);
        }
    }
}
