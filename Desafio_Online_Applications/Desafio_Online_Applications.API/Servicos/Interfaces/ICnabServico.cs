using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos.Interfaces
{
    public interface ICnabServico
    {
        Task ProcessarCnabAsync(byte[] arquivo);
        Task<ErrosViewModel> ListarErrosAsync();
    }
}
