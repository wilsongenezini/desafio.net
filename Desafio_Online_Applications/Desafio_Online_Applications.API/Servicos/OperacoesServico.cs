using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos
{
    public class OperacoesServico : IOperacoesServico
    {
        private readonly IOperacaoFinanceiraRepositorio _operacaoFinanceira;

        public OperacoesServico(IOperacaoFinanceiraRepositorio operacaoFinanceira)
        {
            _operacaoFinanceira = operacaoFinanceira;
        }

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
