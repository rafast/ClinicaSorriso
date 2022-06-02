
using ClinicaSorriso.Models;
using ClinicaSorriso.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaSorriso.Services
{
    public class ConsultaService : IConsultaService
    {
        private ConsultaRepositoryInMemory _consultaRepositoryInMemory { get; set; }

        public ConsultaService(ConsultaRepositoryInMemory consultaRepositoryInMemory)
        {
            _consultaRepositoryInMemory = consultaRepositoryInMemory;
        }

        public void CadastrarConsulta(Consulta consulta)
        {
            if (consulta.Paciente == null)
            {
                throw new ArgumentException("Consulta sem Paciente!!");
            }
            _consultaRepositoryInMemory.Salvar(consulta);

        }

        
        public void ExcluirConsulta(Consulta consulta, List<string> listaDeDados)
        {         
            string data = consulta.Data.ToString("dd/MM/yyyy");
            string hora = consulta.HoraInicio;

            if (hora.Contains(":")) hora = hora.Replace(":", "");

            if (data != listaDeDados[0] || hora != listaDeDados[1])
            {
                throw new ApplicationException("Agendamento não encontrado. ");
            }
            _consultaRepositoryInMemory.Deletar(consulta);
        }

        public List<Consulta> ListarConsultas()
        {
            return _consultaRepositoryInMemory.ListarTodos().OrderBy(c => c.Data).ToList();
        }

        public List<Consulta> ListarConsultasDoDia(DateTime dataHj)
        {
            return _consultaRepositoryInMemory.ListarTodos().Where(c => c.Data == dataHj).ToList();
        }

        public bool TemChoqueDeHorario(Consulta novaConsulta)
        {
            var consultasDoDia = ListarConsultas().Where(c => c.Data.Date == novaConsulta.Data.Date)
                                                  .ToList();
            foreach (var consulta in consultasDoDia)
            {
                if (novaConsulta.TemChoqueDeHorario(consulta))
                {
                    novaConsulta.Paciente.CancelarConsulta();
                    throw new ApplicationException(" já existe uma consulta agendada nesta data/hora ");
                }
            }
            return false;
        }

        public void ExcluirConsultasDoPaciente(Paciente paciente)
        {
            var consultasDoPaciente = ListarConsultas().Where(c => c.Paciente.Cpf == paciente.Cpf);

            if (consultasDoPaciente.Count() > 0)
            {
                foreach (var consulta in consultasDoPaciente)
                {
                    _consultaRepositoryInMemory.Deletar(consulta);
                }
            }
        }

    }
}
