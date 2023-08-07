using Desafio_Online_Applications.API.Configuracao;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Online_Applications.API.Repositorios
{
    public class OperacaoFinanceira : IOperacaoFinanceira
    {
        private readonly Contexto _contexto;

        public OperacaoFinanceira(Contexto contexto) 
        {
            _contexto = contexto;
        }
        public async Task InserirOperacoesAsync(List<Operacoes> operacoes)
        {
            await _contexto.Operacoes.AddRangeAsync(operacoes);
            await _contexto.SaveChangesAsync();
        }
        public async Task<List<Operacoes>> ConsultarOperacoesAsync()
        {
            return await _contexto.Operacoes.ToListAsync();
        }
    }
}
