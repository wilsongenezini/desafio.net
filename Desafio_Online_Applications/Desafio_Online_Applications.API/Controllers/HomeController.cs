using Microsoft.AspNetCore.Mvc;

namespace Desafio_Online_Applications.API.Controllers
{
    /// <summary>
    /// Classe padrão do ASP.NET MVC para Controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Método construtor da classe HomeController.
        /// </summary>
        /// <param name="logger">Registrar informações de log.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Assinatura do método de ação Index.
        /// </summary>
        /// <returns>Retorno da view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Assinatura do método de ação Error.
        /// </summary>
        /// <returns>Retorno da view de Error agora.</returns>
        public IActionResult Error()
        {
            return View();
        }
    }
}