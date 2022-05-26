using System;
namespace ClinicaSorriso.Models
{
    public class Paciente
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public Consulta ConsultaMarcada { get; set; } = null;

        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public int GetIdade()
        {
            return DateTime.Today.Year - DataNascimento.Year;
        }
    }
}
