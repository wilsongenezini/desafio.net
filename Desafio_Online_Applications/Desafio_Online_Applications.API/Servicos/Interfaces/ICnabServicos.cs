namespace Desafio_Online_Applications.API.Servicos.Interfaces
{
    public interface ICnabServicos
    {
        Task ProcessarCnabAsync(byte[] arquivo);
    }
}
