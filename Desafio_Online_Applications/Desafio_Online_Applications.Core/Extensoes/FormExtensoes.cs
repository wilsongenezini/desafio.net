using Microsoft.AspNetCore.Http;

namespace Desafio_Online_Applications.Core.Extensoes
{
    /// <summary>
    /// Classe para formatar o arquivo.
    /// </summary>
    public static class FormExtensoes
    {
        /// <summary>
        /// Método que trata o arquivo upado para um byte[]
        /// </summary>
        /// <param name="arquivo">Arquivo de texto upado.</param>
        /// <returns>Retorna o arquivo em formato byte[]</returns>
        public static byte[] ToByteArray(this IFormFile arquivo)
        {
            using (var ms = new MemoryStream())
            {
                arquivo.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
