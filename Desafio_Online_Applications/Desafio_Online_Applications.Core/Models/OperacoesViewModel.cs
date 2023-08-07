using Desafio_Online_Applications.Core.Entidades;

namespace Desafio_Online_Applications.Core.Models
{
    /// <summary>
    /// Operações com as listagens bem sucedidas e com o saldo das lojas.
    /// </summary>
    public class OperacoesViewModel
    {
        public List<List<Operacoes>> Operacoes { get; set; }
        public List<SomaLojas> SomaLojas { get; set; }
    }
}
