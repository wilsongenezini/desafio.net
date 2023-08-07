namespace Desafio_Online_Applications.Core.Extensoes
{
    /// <summary>
    /// Operações com as strings do arquivo.
    /// </summary>
    public static class StringExtensoes
    {
        /// <summary>
        /// Verificar se o CPF é válido.
        /// </summary>
        /// <param name="cpf">Caracteres que irão ser tratados.</param>
        /// <returns>Se o CPF é válido ou inválido.</returns>
        public static bool IsCPFValido(this string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (tempCpf[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            resto = (resto < 2) ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (tempCpf[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            resto = (resto < 2) ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Formatar o CPF com pontos e traço.
        /// </summary>
        /// <param name="dado">Caracteres que irão ser tratados.</param>
        /// <returns>Retorna o CPF com pontos e traço.</returns>
        public static string FormatarCPF(this string dado)
        {
            return string.Format("{0:000}.{1:000}.{2:000}-{3:00}", Convert.ToInt32(dado.Substring(0, 3)), 
                                                                   Convert.ToInt32(dado.Substring(3, 3)), 
                                                                   Convert.ToInt32(dado.Substring(6, 3)), 
                                                                   Convert.ToInt32(dado.Substring(9, 2)));
        }
    }
}
