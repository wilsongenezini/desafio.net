using Microsoft.AspNetCore.Http;

namespace Desafio_Online_Applications.Core.Extensoes
{
    public static class FormExtensoes
    {
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
