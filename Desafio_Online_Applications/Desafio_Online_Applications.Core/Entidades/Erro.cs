using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_Online_Applications.Core.Entidades
{
    public class Erro
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(13)")]
        public string Tipo { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Data { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Valor { get; set; }

        [Column(TypeName = "nvarchar(14)")]
        public string Cpf { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string Cartao { get; set; }

        [Column(TypeName = "nvarchar(14)")]
        public string DonoLoja { get; set; }

        [Column(TypeName = "nvarchar(18)")]
        public string NomeLoja { get; set; }
    }
}
