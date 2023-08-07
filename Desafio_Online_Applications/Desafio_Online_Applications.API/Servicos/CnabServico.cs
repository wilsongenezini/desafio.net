using Desafio_Online_Applications.Core.Extensoes;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;
using System.Text;
using Desafio_Online_Applications.Core.Enums;

namespace Desafio_Online_Applications.API.Servicos
{
    public class CnabServico : ICnabServico
    {
        private readonly IOperacaoFinanceiraRepositorio _operacaoFinanceira;
        private readonly ILogger<CnabServico> _logger;
        private readonly IErrosRepositorio _erros;

        public CnabServico(IOperacaoFinanceiraRepositorio operacaoFinanceira, IErrosRepositorio erros, ILogger<CnabServico> logger) 
        {
            _operacaoFinanceira = operacaoFinanceira;
            _logger = logger;
            _erros = erros;
        }

        public async Task<ErrosViewModel> ListarErrosAsync()
        {
            var erros = await _erros.ConsultarErrosAsync();
            var errosViewModel = new ErrosViewModel
            {
                OperacoesComErro = erros
            };

            return errosViewModel;
        }

        public async Task ProcessarCnabAsync(byte[] arquivo)
        {
            var strArquivo = Encoding.UTF8.GetString(arquivo);

            var linhas = strArquivo.Split('\n');

            List<Operacoes> operacoes = new();

            List<Erro> operacoesComErros = new();

            foreach (string linha in linhas)
            {
                try
                {
                    if (string.IsNullOrEmpty(linha))
                        continue;

                    var operacao = new Operacoes
                    {
                        Tipo = ObterTipoTransacao(linha.Substring(0, 1)),
                        Data = ObterDataValidada(linha.Substring(1, 8)),
                        Valor = ObterValorFormatado(linha.Substring(9, 10)),
                        Cpf = ObterCPFValidade(linha.Substring(19, 11)),
                        Cartao = linha.Substring(30, 12),
                        DonoLoja = linha.Substring(42, 14),
                        NomeLoja = linha.Substring(56, 18)
                    };
                    operacoes.Add(operacao);
                }
                catch (Exception ex)
                {
                    var erro = new Erro
                    {
                        Tipo = linha.Substring(0, 1),
                        Data = linha.Substring(1, 8),
                        Valor = ObterValorFormatado(linha.Substring(9, 10)),
                        Cpf = linha.Substring(19, 11).FormatarCPF(),
                        Cartao = linha.Substring(30, 12),
                        DonoLoja = linha.Substring(42, 14),
                        NomeLoja = linha.Substring(56, 18),
                        ErroMotivo = ex.Message
                    };

                    operacoesComErros.Add(erro);

                    _logger.LogError(ex.Message);

                    continue;
                }
            }

            await _operacaoFinanceira.InserirOperacoesAsync(operacoes);
            await _erros.InserirErrosAsync(operacoesComErros);
        }

        private TipoTransacaoEnum ObterTipoTransacao(string dado)
        {
            switch (dado)
            {
                case "1":
                    return TipoTransacaoEnum.Debito;
                case "2":
                    return TipoTransacaoEnum.Credito;
                case "3":
                    return TipoTransacaoEnum.PIX;
                case "4":
                    return TipoTransacaoEnum.Financiamento;
                default:
                    throw new ArgumentException("Tipo de transação informado é inválido.");
            }
        }

        private string ObterCPFValidade(string dado)
        {
            if (!dado.IsCPFValido())
                throw new ArgumentException("CPF Inválido");

            return dado.FormatarCPF();
        }

        private DateTime ObterDataValidada(string dado)
        {
            try
            {
                var data = DateTime.ParseExact(dado, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                return data;
            }
            catch 
            {
                throw new ArgumentException("Data Inválida");
            }            
        }

        private decimal ObterValorFormatado(string dado)
        {
            return Convert.ToDecimal(dado) / 100;
        }
    }
}
