using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    public class ErrosController : Controller
    {
        private readonly ICnabServicos _cnabServicos;

        public ErrosController(ICnabServicos cnabServicos)
        {
            _cnabServicos = cnabServicos;
        }

        [HttpGet()]
        public async Task<IActionResult> IndexAsync()
        {
            ErrosViewModel errosViewModel = await _cnabServicos.ListarErros();

            return View("ListagemErros", errosViewModel);
        }
    }
}
