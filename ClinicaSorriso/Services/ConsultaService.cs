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

        // Recebe uma consulta e salva no repositorio
        public void CadastrarConsulta(Consulta consulta)
        {
            _consultaRepositoryInMemory.Salvar(consulta);

        }

        // Recebe uma consulta e exclui do repositorio
        public void ExcluirConsulta(Consulta consulta)
        {                    
            _consultaRepositoryInMemory.Deletar(consulta);
        }

        //Retorna uma lista com todas as consultas do repositorio ordenadas por data
        public List<Consulta> ListarConsultas()
        {
            return _consultaRepositoryInMemory.ListarTodos()
                                              .OrderBy(c => c.Data)
                                              .ToList();
        }

        //Recebe uma data e retorna uma lista com todas as consultas agendadas para este dia
        public List<Consulta> ListarConsultasDoDia(DateTime dataAgendamento)
        {
            return _consultaRepositoryInMemory.ListarTodos()
                                              .Where(c => c.Data.Date == dataAgendamento.Date)
                                              .ToList();
        }

        //Recebe uma consulta e retorna se há conflito de horário desta consulta com as demais consultas do mesmo dia
        public bool TemConflitoDeHorario(Consulta novaConsulta)
        {
            var consultasDoDia = ListarConsultasDoDia(novaConsulta.Data);
            foreach (var consulta in consultasDoDia)
            {
                if (novaConsulta.TemConflitoDeHorario(consulta))
                {
                    return true;
                }
            }
            return false;
        }

        //Recebe um paciente e exclui todas as consultas vinculadas a ele
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

        //Recebe uma data inicial e final e retorna uma lista com todas as consultas agendadas nesse periodo
        public List<Consulta> ListarConsultasPorPeriodo(DateTime dtInicio, DateTime dtFim)
        {
            return _consultaRepositoryInMemory.ListarTodos()
                                              .Where(consulta => (consulta.Data.Date >= dtInicio.Date) &
                                                                  consulta.Data.Date <= dtFim.Date)
                                              .ToList();
        }

        //Recebe uma consulta e caso exista no repositorio, retorna ela
        public Consulta BuscarConsulta(Consulta consulta)
        {
            return _consultaRepositoryInMemory.ListarTodos()
                                              .Where(c => c.Paciente.Cpf == consulta.Paciente.Cpf &
                                                          c.Data.Date == consulta.Data.Date &
                                                          c.HoraInicio == consulta.HoraInicio)
                                              .SingleOrDefault();
        }
    }
}
