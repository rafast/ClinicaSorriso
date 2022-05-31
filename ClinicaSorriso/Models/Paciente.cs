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
            return DateTime.Today.Year - DataNascimento.Year;
        }

        public void MarcarConsulta(Consulta consulta)
        {
            ConsultaMarcada = consulta;
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
