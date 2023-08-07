using Desafio_Online_Applications.API.Configuracao;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Online_Applications.API.Repositorios
{
    /// <summary>
    /// Classe que grava as operações com erros.
    /// </summary>
    public class ErrosRepositorio : IErrosRepositorio
    {
        private readonly Contexto _contexto;

        /// <summary>
        /// Método construtor da classe.
        /// </summary>
        /// <param name="contexto">Para utilizar métodos do Contexto.</param>
        public ErrosRepositorio(Contexto contexto) 
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Método que irá gravar e salvar as operações com erros no banco de dados.
        /// </summary>
        /// <param name="erros">Lista das operações com erros.</param>
        public async Task InserirErrosAsync(List<Erro> erros)
        {
            await _contexto.Erros.AddRangeAsync(erros);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// Método que consulta ao banco de dados.
        /// </summary>
        /// <returns>Após a conclusão da consulta, a lista retorna.</returns>
        public async Task<List<Erro>> ConsultarErrosAsync()
        {
            return await _contexto.Erros.ToListAsync();
        }
    }
}
