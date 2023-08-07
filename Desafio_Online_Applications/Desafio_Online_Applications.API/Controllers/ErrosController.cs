using Desafio_Online_Applications.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    public class ErrosController : Controller
    {
        private readonly ICnabServico _cnabServicos;

        public ErrosController(ICnabServico cnabServicos)
        {
            _cnabServicos = cnabServicos;
        }

        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            var errosViewModel = await _cnabServicos.ListarErrosAsync();

            return View("ListagemErros", errosViewModel);
        }
    }
}
