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
            throw new NotImplementedException();
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
