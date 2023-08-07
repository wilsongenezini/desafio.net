using Desafio_Online_Applications.Core.Models;
namespace Desafio_Online_Applications.API.Servicos.Interfaces
{
    public interface ICnabServicos
    {
        Task ProcessarCnabAsync(byte[] arquivo);
        Task<ErrosViewModel> ListarErros();
    }
}
