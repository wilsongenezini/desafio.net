using Desafio_Online_Applications.API.Configuracao;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Online_Applications.API.Repositorios
{
    /// <summary>
    /// Classe que trata da lista de operações sem erros.
    /// </summary>
    public class OperacaoFinanceiraRepositorio : IOperacaoFinanceiraRepositorio
    {
        private readonly Contexto _contexto;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="contexto">Para utilizar métodos do Contexto.</param>
        public OperacaoFinanceiraRepositorio(Contexto contexto) 
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Método que irá gravar e salvar as operações com erros no banco de dados.
        /// </summary>
        /// <param name="operacoes">Lista de operações.</param>
        public async Task InserirOperacoesAsync(List<Operacoes> operacoes)
        {
            await _contexto.Operacoes.AddRangeAsync(operacoes);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// Método que consulta ao banco de dados.
        /// </summary>
        /// <returns>Após a conclusão da consulta, a lista retorna.</returns>
        public async Task<List<Operacoes>> ConsultarOperacoesAsync()
        {
            return await _contexto.Operacoes.ToListAsync();
        }
    }
}
