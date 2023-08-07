using Desafio_Online_Applications.Core.Entidades;

namespace Desafio_Online_Applications.API.Repositorios.Interface
{
    public interface IOperacaoFinanceiraRepositorio
    {
        Task InserirOperacoesAsync(List<Operacoes> operacoes);
        Task<List<Operacoes>> ConsultarOperacoesAsync();
    }
}
