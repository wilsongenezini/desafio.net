using Desafio_Online_Applications.API.Servicos.Interfaces;
using Desafio_Online_Applications.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace Desafio_Online_Applications.API.Servicos
{
    public class CnabServico : ICnabServicos
    {
        private readonly MeuDbContext _dbContext;

        public CnabServico(MeuDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task ProcessarCnabAsync(byte[] arquivo)
        {
            string strArquivo = Encoding.UTF8.GetString(arquivo);
            Console.Write(strArquivo);

            DateTime data;

            string[] linhas = strArquivo.Split('\n');
            

            foreach (string linha in linhas)
            {
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

                if (DateTime.TryParseExact(dataOcorrencia, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out data))
                    dataOcorrencia = data.ToString("dd/MM/yyy");
                else
                    dataOcorrencia = "Formato de data inválida."; //Armazenar operações com erros

                decimal valorMovimentacao = Convert.ToDecimal(linha.Substring(9, 10)) / 100;

                string cpfBeneficiario = linha.Substring(19, 11);
                string cpfBenefFormatado = string.Format("{0:000}.{1:000}.{2:000}-{3:00}", Convert.ToInt32(cpfBeneficiario.Substring(0, 3)), Convert.ToInt32(cpfBeneficiario.Substring(3, 3)), Convert.ToInt32(cpfBeneficiario.Substring(6, 3)), Convert.ToInt32(cpfBeneficiario.Substring(9, 2)));

                static bool VerificaCpf(string cpf)
                {
                    int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                    int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                    string tempCpf;
                    string digito;
                    int soma;
                    int resto;
                    cpf = cpf.Trim();
                    cpf = cpf.Replace(".", "").Replace("-", "");
                    if (cpf.Length != 11)
                        return false;
                    tempCpf = cpf.Substring(0, 9);
                    soma = 0;

                    for (int i = 0; i < 9; i++)
                        soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                    resto = soma % 11;
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;
                    digito = resto.ToString();
                    tempCpf = tempCpf + digito;
                    soma = 0;
                    for (int i = 0; i < 10; i++)
                        soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                    resto = soma % 11;
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;
                    digito = digito + resto.ToString();
                    return cpf.EndsWith(digito);
                }

                if (!VerificaCpf(cpfBenefFormatado))
                    cpfBenefFormatado = "CPF inválido."; //Armazenar operações com erros

                string cartaoTransacao = linha.Substring(30, 12);

                string nomeRepresentante = linha.Substring(42, 14);

                string nomeLoja = linha.Substring(56, 18);

                Operacoes model = new Operacoes
                (
                    tipoTransacao,
                    dataOcorrencia,
                    valorMovimentacao,
                    cpfBenefFormatado,
                    cartaoTransacao,
                    nomeRepresentante,
                    nomeLoja
                );
                _dbContext.ListagemOperacoes.Add(model);
                _dbContext.SaveChanges();


                Console.WriteLine();    
                Console.WriteLine("Tipo de transação: " + tipoTransacao);
                Console.WriteLine("Data da ocorrência: " + dataOcorrencia);
                Console.WriteLine("Valor da movimentação: R$ " + valorMovimentacao);
                Console.WriteLine("CPF do beneficiário: " + cpfBenefFormatado);
                Console.WriteLine("Cartão utilizado na transação: " + cartaoTransacao);
                Console.WriteLine("Nome do representante da loja: " + nomeRepresentante);
                Console.WriteLine("Nome da loja: " + nomeLoja);
            }
        }
    }
}
