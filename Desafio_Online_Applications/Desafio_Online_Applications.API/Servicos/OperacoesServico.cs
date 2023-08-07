using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos
{
    /// <summary>
    /// Classe que trata as operações que serão apresentadas na página web.
    /// </summary>
    public class OperacoesServico : IOperacoesServico
    {
        private readonly IOperacaoFinanceiraRepositorio _operacaoFinanceira;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="operacaoFinanceira">Para utilizar a interface.</param>
        public OperacoesServico(IOperacaoFinanceiraRepositorio operacaoFinanceira)
        {
            _operacaoFinanceira = operacaoFinanceira;
        }

        /// <summary>
        /// Lista que recebe todas as operações sem erros.
        /// </summary>
        /// <returns>Retorna a lista das operações e também o saldo total de cada loja.</returns>
        public async Task<OperacoesViewModel> VisualizarListaOperacoesAsync()
        {
            var operacoes = await _operacaoFinanceira.ConsultarOperacoesAsync();
            var listaListaOperacoes = ListarPorLojas(operacoes);
            var somaLojas = SomaValorLojas(listaListaOperacoes);

            var operacoesViewModel = new OperacoesViewModel
            {
                Operacoes = listaListaOperacoes,
                SomaLojas = somaLojas
            };

            return operacoesViewModel;
        }

        /// <summary>
        /// Método que pega a lista de todas as operações e filtra uma segunda lista com as lojas.
        /// </summary>
        /// <param name="operacoes">Lista de operações para ser tratada.</param>
        /// <returns>Retorna uma lista de listas, agora separada por lojas.</returns>
        private List<List<Operacoes>> ListarPorLojas(List<Operacoes> operacoes)
        {
            var listaAux = operacoes;

            var listaAuxCopia = listaAux.ToList();

            var listaResultado = new List<List<Operacoes>>();

            foreach (Operacoes operacao in operacoes)
            {
                var listaFiltradaPorLoja = new List<Operacoes>();
                listaAux = listaAuxCopia.ToList();

                foreach (Operacoes operacaoAux in listaAux)
                {
                    if (operacaoAux.Equals(operacao) || operacaoAux.NomeLoja.Equals(operacao.NomeLoja)) 
                    {
                        listaFiltradaPorLoja.Add(operacaoAux);
                        listaAuxCopia.Remove(operacaoAux);
                    }
                }

                listaResultado.Add(listaFiltradaPorLoja);
            }

            return listaResultado;
        }
        
        /// <summary>
        /// Método que soma o saldo das operações por loja.
        /// </summary>
        /// <param name="listaListaOperacoes">Recebe a lista de listas do método anterior.</param>
        /// <returns>Retorna uma lista com esse saldo calculado.</returns>
        private List<SomaLojas> SomaValorLojas(List<List<Operacoes>> listaListaOperacoes)
        {
            var listaSomaLojas = new List<SomaLojas>();

            foreach (List<Operacoes> operacoes in listaListaOperacoes)
            {
                var somaLoja = new SomaLojas();
                if (operacoes.Count > 0)
                {
                    somaLoja.NomeLoja = operacoes[0].NomeLoja;
                    somaLoja.ValorSomaLoja = CalculaValorPorLoja(operacoes);
                    listaSomaLojas.Add(somaLoja);
                }
            }

            return listaSomaLojas;
        }

        /// <summary>
        /// Método que soma o saldo das operações por loja.
        /// </summary>
        /// <param name="operacoes">Recebe a lista de listas do método ListarPorLojas.</param>
        /// <returns>Retorna o saldo calculado em tipo decimal.</returns>
        private decimal CalculaValorPorLoja(List<Operacoes> operacoes)
        {
            var somaLoja = 0m;

            foreach (Operacoes operacao in operacoes)
            {
                somaLoja += operacao.Valor;
            }

            return somaLoja;
        }
    }
}
