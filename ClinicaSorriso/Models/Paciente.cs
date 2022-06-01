using System;
namespace ClinicaSorriso.Models
{
    public class Paciente
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public Consulta ConsultaMarcada { get; set; }

        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public int GetIdade()
        {
            var dataHoje = DateTime.Today;
            var idade = dataHoje.Year - DataNascimento.Year;

            if (DataNascimento > dataHoje.AddYears(-idade))
            {
                idade--;
            }
            return idade;
        }

        public void MarcarConsulta(Consulta consulta)
        {
            ConsultaMarcada = consulta;
        }

        public void CancelarConsulta()
        {
            ConsultaMarcada = null;
        }

        public bool TemConsultaFutura()
        {
            if (ConsultaMarcada != null)
            {
                return ConsultaMarcada.Data > DateTime.Now;
            }
            return false;
        }
    }
}
