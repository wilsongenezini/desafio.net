using Desafio_Online_Applications.API.Servicos;
using Microsoft.EntityFrameworkCore;
using Desafio_Online_Applications.API.Repositorios;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.API.Servicos.Interfaces;

namespace Desafio_Online_Applications.API.Configuracao
{
    /// Classe de injeção de dependência da API.
    public static class InjecaoDependencia
    {
        /// Método estático que adiciona o DBContext do SQLServer e a injeção de dependência das classes de implementação e repositórios.
        
        /// <param name="services">Recebe um ServiceCollection da Program para adicionar as injeções.</param>
        /// <param name="configuration">Recebe um configuration do Program para obter dados do AppSettings.</param>
        public static void InjetarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Contexto>(contexto => contexto.UseSqlServer(configuration.GetConnectionString("MinhaConexao")));

            services.AddScoped<ICnabServico, CnabServico>();
            services.AddScoped<IOperacaoFinanceiraRepositorio, OperacaoFinanceiraRepositorio>();
            services.AddScoped<IErrosRepositorio, ErrosRepositorio>();
            services.AddScoped<IOperacoesServico, OperacoesServico>();
        }
    }
}
