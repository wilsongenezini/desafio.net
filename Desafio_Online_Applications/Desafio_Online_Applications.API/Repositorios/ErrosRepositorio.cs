using Desafio_Online_Applications.API.Configuracao;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Online_Applications.API.Repositorios
{
    public class ErrosRepositorio : IErrosRepositorio
    {
        private readonly Contexto _contexto;

        public ErrosRepositorio(Contexto contexto) 
        {
            _contexto = contexto;
        }

        public async Task InserirErrosAsync(List<Erro> erros)
        {
            await _contexto.Erros.AddRangeAsync(erros);
            await _contexto.SaveChangesAsync();
        }

        public async Task<List<Erro>> ConsultarErrosAsync()
        {
            return await _contexto.Erros.ToListAsync();
        }
    }
}
