using Microsoft.EntityFrameworkCore;
using Desafio_Online_Applications.Core.Entidades;

namespace Desafio_Online_Applications.API.Configuracao
{
    /// <summary>
    ///  A classe Contexto atua como a ponte entre as classes e o banco de dados.
    /// </summary>
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> contexto) : base(contexto)
        {
        }

        public DbSet<Operacoes> Operacoes { get; set; }
        public DbSet<Erro> Erros { get; set; }
    }
}
