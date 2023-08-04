using Desafio_Online_Applications.API.Servicos.Interfaces;
using System.Text;

namespace Desafio_Online_Applications.API.Servicos
{
    public class CnabServico : ICnabServicos
    {
        public async Task ProcessarCnabAsync(byte[] arquivo)
        {
            string strArquivo;
            strArquivo = Encoding.UTF8.GetString(arquivo);
            Console.Write(strArquivo);

            /*foreach (byte b in arquivo)
            {
                Console.Write(b.ToString(strArquivo)); 
            }*/
        }
    }
}
