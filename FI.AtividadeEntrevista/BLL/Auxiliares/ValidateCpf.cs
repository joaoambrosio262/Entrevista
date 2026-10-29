using System;
using System.Linq;

namespace FI.AtividadeEntrevista.BLL
{
    public static class ValidateCpf
    {
        /// <summary>
        /// Valida se uma cadeia de caracteres representa um CPF válido segundo o algoritmo oficial
        /// </summary>
        /// <param name="cpf">Número do CPF formatado ou somente dígitos</param>
        /// <returns>True se válido, False caso contrário</returns>
        public static bool Validador(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            string valor = new string(cpf.Where(char.IsDigit).ToArray());

            if (valor.Length != 11)
                return false;

            // CPFs com todos os dígitos iguais são inválidos
            if (valor.Distinct().Count() == 1)
                return false;

            // Validação do 1º dígito
            int[] primeiroMultiplicador = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (valor[i] - '0') * primeiroMultiplicador[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if ((valor[9] - '0') != digito1)
                return false;

            // Validação do 2º dígito
            int[] segundoMultiplicador = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (valor[i] - '0') * segundoMultiplicador[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return (valor[10] - '0') == digito2;
        }

        /// <summary>
        /// Formata um CPF no padrão 000.000.000-00
        /// </summary>
        public static string FormatarValor(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            string valor = new string(cpf.Where(char.IsDigit).ToArray());
            if (valor.Length != 11)
                return cpf;

            return Convert.ToUInt64(valor).ToString(@"000\.000\.000\-00");
        }
    }
}
