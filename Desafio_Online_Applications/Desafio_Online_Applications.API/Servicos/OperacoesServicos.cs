using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;

namespace Desafio_Online_Applications.API.Servicos
{
    public class OperacoesServicos : IOperacoesServicos
    {
        private readonly IOperacaoFinanceira _operacaoFinanceira;

        public OperacoesServicos(IOperacaoFinanceira operacaoFinanceira)
        {
            _operacaoFinanceira = operacaoFinanceira;
        }
                
        public async Task<List<List<Operacoes>>> ListarPorLojas(List<Operacoes> operacoes)
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

        public async Task<decimal> calculaValorPorLoja(List<Operacoes> operacoes)
        {
            decimal somaLoja = 0;

            foreach (Operacoes operacao in operacoes)
            {
                somaLoja += operacao.Valor;
            }
            return somaLoja;
        }

        public async Task<List<SomaLojas>> SomaValorLojas(List<List<Operacoes>> listaListaOperacoes)
        {
            List<SomaLojas> listaSomaLojas = new List<SomaLojas>();

            //SomaLojas somaLoja = new SomaLojas();

            foreach (List<Operacoes> operacoes in listaListaOperacoes)
            {
                SomaLojas somaLoja = new SomaLojas();
                if (operacoes.Count > 0)
                {
                    somaLoja.NomeLoja = operacoes[0].NomeLoja;
                    somaLoja.ValorSomaLoja = await calculaValorPorLoja(operacoes);
                    listaSomaLojas.Add(somaLoja);
                }
            }
            return listaSomaLojas;
        }

        public async Task<OperacoesViewModel> VisualizarListaOperacoes()
        {
            List<Operacoes> operacoes = await _operacaoFinanceira.ConsultarOperacoesAsync();
            List<List<Operacoes>> listaListaOperacoes = await ListarPorLojas(operacoes);
            List<SomaLojas> somaLojas = await SomaValorLojas(listaListaOperacoes);
            OperacoesViewModel operacoesViewModel = new OperacoesViewModel
            {
                Operacoes = listaListaOperacoes,
                SomaLojas = somaLojas
            };

            return operacoesViewModel;
        }
    }
}
