using Desafio_Online_Applications.Core.Extensoes;
using Desafio_Online_Applications.API.Repositorios.Interface;
using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Entidades;
using Desafio_Online_Applications.Core.Models;
using System.Text;

namespace Desafio_Online_Applications.API.Servicos
{
    public class CnabServico : ICnabServicos
    {
        private readonly IOperacaoFinanceira _operacaoFinanceira;
        private readonly ILogger<CnabServico> _logger;
        private readonly IErros _erros;

        public CnabServico(IOperacaoFinanceira operacaoFinanceira, IErros erros, ILogger<CnabServico> logger) 
        {
            _operacaoFinanceira = operacaoFinanceira;
            _logger = logger;
            _erros = erros;
        }

        public async Task<ErrosViewModel> ListarErros()
        {
            List<Erro> erros = await _erros.ConsultarErrosAsync();
            ErrosViewModel errosViewModel = new ErrosViewModel
            {
                OperacoesComErro = erros
            };
            return errosViewModel;
        }

        public async Task ProcessarCnabAsync(byte[] arquivo)
        {
            string strArquivo = Encoding.UTF8.GetString(arquivo);

            string[] linhas = strArquivo.Split('\n');

            List<Operacoes> operacoes = new();

            List<Erro> operacoesComErros = new();

            foreach (string linha in linhas)
            {
                try
                {
                    if (string.IsNullOrEmpty(linha))
                        continue;

                    string tipoTransacao = linha.Substring(0, 1);

                    if (tipoTransacao == "1")
                        tipoTransacao = "Débito";
                    else if (tipoTransacao == "2")
                        tipoTransacao = "Crédito";
                    else if (tipoTransacao == "3")
                        tipoTransacao = "PIX";
                    else if (tipoTransacao == "4")
                        tipoTransacao = "Financiamento";

                    string dataOcorrencia = linha.Substring(1, 8);

                    if (DateTime.TryParseExact(dataOcorrencia, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime data))
                        dataOcorrencia = data.ToString("dd/MM/yyyy");
                    else
                        throw new ArgumentException("Data inválida"); //adiciinado com o cris
                    
                    decimal valorMovimentacao = Convert.ToDecimal(linha.Substring(9, 10)) / 100;

                    string cpfBeneficiario = ObterCPFValidade(linha.Substring(19, 11));

                    string cpfBenefFormatado = string.Format("{0:000}.{1:000}.{2:000}-{3:00}", Convert.ToInt32(cpfBeneficiario.Substring(0, 3)), Convert.ToInt32(cpfBeneficiario.Substring(3, 3)), Convert.ToInt32(cpfBeneficiario.Substring(6, 3)), Convert.ToInt32(cpfBeneficiario.Substring(9, 2)));
                                                                               
                    string cartaoTransacao = linha.Substring(30, 12);

                    string nomeRepresentante = linha.Substring(42, 14);

                    string nomeLoja = linha.Substring(56, 18);

                    Operacoes operacao = new Operacoes
                    {
                        Tipo = tipoTransacao,
                        Data = dataOcorrencia,
                        Valor = valorMovimentacao,
                        Cpf = cpfBeneficiario,
                        Cartao = cartaoTransacao,
                        DonoLoja = nomeRepresentante,
                        NomeLoja = nomeLoja
                    };
                    operacoes.Add(operacao);
                }
                catch (Exception ex)
                {
                    var erro = new Erro();
                    erro.Tipo = linha.Substring(0, 1);
                    erro.Data = linha.Substring(1, 8);
                    erro.Valor = linha.Substring(9, 10);
                    erro.Cpf = linha.Substring(19, 11);
                    erro.Cartao = linha.Substring(30, 12);
                    erro.DonoLoja = linha.Substring(42, 14);
                    erro.NomeLoja = linha.Substring(56, 18);

                    operacoesComErros.Add(erro);
                    _logger.LogError(ex.Message);

                    continue;
                }
                
            }
            await _operacaoFinanceira.InserirOperacoesAsync(operacoes);
            await _erros.InserirErrosAsync(operacoesComErros);
        }

        private string ObterCPFValidade(string dado)
        {
            if (!dado.VerificaCpf())
                throw new ArgumentException("CPF Inválido");

            return dado;
        }
    }
}
