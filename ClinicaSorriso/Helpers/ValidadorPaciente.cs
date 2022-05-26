using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ClinicaSorriso.Helpers
{
    public class ValidadorPaciente
    {
        public Dictionary<string,string> erros { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DtNascimento { get; set; }

        public ValidadorPaciente(string nome, string cpf, string dtNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DtNascimento = dtNascimento;
            erros = new Dictionary<string, string>();
        }

        public bool IsValid()
        {
            return erros.Count == 0;
        }

        public void ValidarDados()
        {
            erros.Clear();
            if (!ValidaNome())
            {
                erros.Add("Nome", "O nome do paciente deve ter pelo menos 5 caracteres.");
            }

            if (!ValidaCPF())
            {
                erros.Add("Cpf", "CPF inválido.");
            }

            if (!ValidaData(out DateTime data))
            {
                erros.Add("DtNascimento", "Data inválida, deve ser fornecida no formato DD/MM/AAAA");
            }
        }

        public void ExibirErros()
        {
            foreach (var erro in erros)
            {
                Console.WriteLine($"Erro: {erro.Value}");
            }
        }

        private bool ValidaNome()
        {
            string padrao = "^([a-zA-Z ]*?)\\s*([a-zA-Z]*)$";
            return Regex.IsMatch(Nome, padrao) && (Nome.Length > 4);
        }

        private bool ValidaCPF()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            string dgsVerificadores;
            int soma;
            int resto;
            Cpf = Cpf.Trim();
            Cpf = Cpf.Replace(".", "").Replace("-", "");
            if ((Cpf == "00000000000") ||
                (Cpf == "11111111111") ||
                (Cpf == "22222222222") ||
                (Cpf == "33333333333") ||
                (Cpf == "44444444444") ||
                (Cpf == "55555555555") ||
                (Cpf == "66666666666") ||
                (Cpf == "77777777777") ||
                (Cpf == "88888888888") ||
                (Cpf == "99999999999"))
            {
                return false;
            }
            if (Cpf.Length != 11)

                return false;

            tempCpf = Cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();
            dgsVerificadores = digito;
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();
            dgsVerificadores += digito;

            if ((dgsVerificadores) == Cpf.Remove(0, 9))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidaData(out DateTime data)
        {
            return DateTime.TryParseExact(DtNascimento,
                                         "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture,
                                         DateTimeStyles.None,
                                         out data);
        }
    }
}
