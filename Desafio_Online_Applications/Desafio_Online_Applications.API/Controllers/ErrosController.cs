using Desafio_Online_Applications.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    /// <summary>
    /// Classe para tratar as operações com erros.
    /// </summary>
    public class ErrosController : Controller
    {
        private readonly ICnabServico _cnabServicos;

        /// <summary>
        /// Método construtor da classe.
        /// </summary>
        /// <param name="cnabServicos">Poder utilizar métodos da interface ICnabServico.</param>
        public ErrosController(ICnabServico cnabServicos)
        {
            _cnabServicos = cnabServicos;
        }

        /// <summary>
        /// Método da interface que trata as operações com erros.
        /// </summary>
        /// <returns>Retornar uma view para procurar por ela na pasta de Views.</returns>
        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            var errosViewModel = await _cnabServicos.ListarErrosAsync();

            return View("ListagemErros", errosViewModel);
        }
    }
}
