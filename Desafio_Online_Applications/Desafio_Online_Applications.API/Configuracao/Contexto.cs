using Microsoft.EntityFrameworkCore;
using Desafio_Online_Applications.Core.Entidades;

namespace Desafio_Online_Applications.API.Configuracao
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> contexto) : base(contexto)
        {
        }

        public DbSet<Operacoes> Operacoes { get; set; }
        public DbSet<Erro> Erros { get; set; }
    }
}
