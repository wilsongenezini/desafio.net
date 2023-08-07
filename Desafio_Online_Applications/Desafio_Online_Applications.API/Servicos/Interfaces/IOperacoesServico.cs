using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos.Interfaces
{
    public interface IOperacoesServico
    {
        Task<OperacoesViewModel> VisualizarListaOperacoesAsync();
    }
}
