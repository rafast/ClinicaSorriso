using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaSorriso.Models;
using ClinicaSorriso.Repositories.InMemory;

namespace ClinicaSorriso.Services
{
    public class PacienteService : IPacienteService
    {
        private PacientesRepositoryInMemory _pacientesRepository { get; set; }

        public PacienteService(PacientesRepositoryInMemory pacientesRepository)
        {
            _pacientesRepository = pacientesRepository;
        }

        public void CadastrarPaciente(Paciente paciente)
        {
            var existePaciente = ConsultarPacientePorCPF(paciente.Cpf);

            if (existePaciente != null)
            {
                throw new ArgumentException("CPF já cadastrado");
            }

            if (paciente.GetIdade() < 13)
            {
                throw new ApplicationException("O dentista não atende crianças. A idade mínima para atendimento é de 13 anos.");
            }
            _pacientesRepository.Salvar(paciente);
        }

        public Paciente ConsultarPacientePorCPF(string cpf)
        {
            return _pacientesRepository.ListarTodos()
                                       .Where(p => p.Cpf == cpf)
                                       .SingleOrDefault();
        }

        public void ExcluirPaciente(Paciente paciente)
        {
            if (paciente.TemConsultaFutura())
            {
                throw new ApplicationException("O paciente possui uma consulta agendada futura.");
            }
            _pacientesRepository.Deletar(paciente);
        }

        public List<Paciente> ListarPacientesPorCPF()
        {
            return _pacientesRepository.ListarTodos().OrderBy(p => p.Cpf).ToList();
        }

        public List<Paciente> ListarPacientesPorNome()
        {
            return _pacientesRepository.ListarTodos().OrderBy(p => p.Nome).ToList();
        }
    }
}
