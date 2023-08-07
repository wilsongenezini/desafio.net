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
                
        public async Task<List<List<Operacoes>>> ListarPorLojasAsync(List<Operacoes> operacoes)
        {
            List<Operacoes> listaAux = operacoes;

            List<Operacoes> listaAuxCopia = listaAux.ToList();

            List<List<Operacoes>> listaResultado = new List<List<Operacoes>>();

            foreach (Operacoes operacao in operacoes)
            {
                List<Operacoes> listaFiltradaPorLoja = new List<Operacoes>();
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

        public async Task<decimal> CalculaValorPorLojaAsync(List<Operacoes> operacoes)
        {
            decimal somaLoja = 0;

            foreach (Operacoes operacao in operacoes)
            {
                somaLoja += operacao.Valor;
            }
            return somaLoja;
        }

        public async Task<List<SomaLojas>> SomaValorLojasAsync(List<List<Operacoes>> listaListaOperacoes)
        {
            List<SomaLojas> listaSomaLojas = new List<SomaLojas>();

            foreach (List<Operacoes> operacoes in listaListaOperacoes)
            {
                SomaLojas somaLoja = new SomaLojas();
                if (operacoes.Count > 0)
                {
                    somaLoja.NomeLoja = operacoes[0].NomeLoja;
                    somaLoja.ValorSomaLoja = await CalculaValorPorLojaAsync(operacoes);
                    listaSomaLojas.Add(somaLoja);
                }
            }
            return listaSomaLojas;
        }

        public async Task<OperacoesViewModel> VisualizarListaOperacoesAsync()
        {
            List<Operacoes> operacoes = await _operacaoFinanceira.ConsultarOperacoesAsync();
            List<List<Operacoes>> listaListaOperacoes = await ListarPorLojasAsync(operacoes);
            List<SomaLojas> somaLojas = await SomaValorLojasAsync(listaListaOperacoes);

            OperacoesViewModel operacoesViewModel = new OperacoesViewModel
            {
                Operacoes = listaListaOperacoes,
                SomaLojas = somaLojas
            };

            return operacoesViewModel;
        }
    }
}
