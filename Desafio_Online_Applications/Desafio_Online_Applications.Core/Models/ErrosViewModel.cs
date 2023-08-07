using Desafio_Online_Applications.Core.Entidades;
namespace Desafio_Online_Applications.Core.Models
{
    /// <summary>
    /// Operações que possuem algum parâmetro com erro.
    /// </summary>
    public class ErrosViewModel 
    {
        public List<Erro> OperacoesComErro { get; set; }
    }
}
