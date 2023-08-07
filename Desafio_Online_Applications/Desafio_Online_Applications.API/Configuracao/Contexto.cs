using Microsoft.EntityFrameworkCore;
using Desafio_Online_Applications.Core.Entidades;

namespace Desafio_Online_Applications.API.Configuracao
{
    /// <summary>
    /// A classe Contexto atua como uma ponte entre as classes e o banco de dados.
    /// </summary>
    public class Contexto : DbContext
    {
        /// <summary>
        /// Construtor da classe contexto 
        /// </summary>
        /// <param name="contexto">Objeto do tipo DbContextOptions passado como parâmetro.</param>
        public Contexto(DbContextOptions<Contexto> contexto) : base(contexto)
        {
        }

        public DbSet<Operacoes> Operacoes { get; set; }
        public DbSet<Erro> Erros { get; set; }
    }
}
