using Desafio_Online_Applications.Core.Entidades;

namespace Desafio_Online_Applications.API.Repositorios.Interface
{
    public interface IErrosRepositorio
    {
        Task InserirErrosAsync(List<Erro> erros);
        Task<List<Erro>> ConsultarErrosAsync();
    }
}
