using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos.Interfaces
{
    public interface IOperacoesServico
    {
        Task<OperacoesViewModel> VisualizarListaOperacoesAsync();
        Task<List<List<Operacoes>>> ListarPorLojasAsync(List<Operacoes> operacoes);
        Task<decimal> CalculaValorPorLojaAsync(List<Operacoes> operacoes);
        Task<List<SomaLojas>> SomaValorLojasAsync(List<List<Operacoes>> listaListaOperacoes);
    }
}
