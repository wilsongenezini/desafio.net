using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos.Interfaces
{
    public interface IOperacoesServicos
    {
        Task<OperacoesViewModel> VisualizarListaOperacoes();
        Task<List<List<Operacoes>>> ListarPorLojas(List<Operacoes> operacoes);
        Task<decimal> calculaValorPorLoja(List<Operacoes> operacoes);
        Task<List<SomaLojas>> SomaValorLojas(List<List<Operacoes>> listaListaOperacoes);
    }
}
