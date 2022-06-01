
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
            if (consulta.Data < DateTime.Now)
            {
                throw new ApplicationException("Só é possivel cancelar agendamentos futuros. ");
                return;
            }

            string data = consulta.Data.ToString("dd/MM/yyyy");
            string hora = consulta.HoraInicio.ToString();
            if (data != listaDeDados[0] || hora != listaDeDados[1])
            {
                throw new ApplicationException(" Agendamento não encontrado. ");
                return;
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
                    return true;
                }
            }
            return false;
        }

    }
}
