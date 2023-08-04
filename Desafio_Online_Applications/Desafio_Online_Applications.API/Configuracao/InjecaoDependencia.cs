using Desafio_Online_Applications.API.Servicos;
using Desafio_Online_Applications.API.Servicos.Interfaces;

namespace Desafio_Online_Applications.API.Configuracao
{
    public static class InjecaoDependencia
    {
        public static void InjetarDependencias(this IServiceCollection services)
        {
            services.AddScoped<ICnabServicos, CnabServico>();
        }
    }
}
