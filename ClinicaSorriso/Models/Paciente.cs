using System;
namespace ClinicaSorriso.Models
{
    // Classe que representa um paciente na aplicação
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

        // Retorna a idade do paciente
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

        // Atribui uma nova consulta ao paciente
        public void MarcarConsulta(Consulta consulta)
        {
            ConsultaMarcada = consulta;
        }

        // Cancela uma consulta futura
        public void CancelarConsulta()
        {
            ConsultaMarcada = null;
        }

        // Retorna se o paciente possui uma consulta marcada futura
        public bool TemConsultaFutura()
        {
            if (ConsultaMarcada != null)
            {
                return ConsultaMarcada.Data.Date >= DateTime.Now.Date;
            }
            return false;
        }
    }
}
