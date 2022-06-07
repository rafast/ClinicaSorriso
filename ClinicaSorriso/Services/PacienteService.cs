using System;
using System.Collections.Generic;
using System.Linq;
using ClinicaSorriso.Models;
using ClinicaSorriso.Repositories.InMemory;

namespace ClinicaSorriso.Services
{
    // Classe que implementa a interface de serviços do Paciente
    public class PacienteService : IPacienteService
    {
        private PacientesRepositoryInMemory _pacientesRepository { get; set; }

        public PacienteService(PacientesRepositoryInMemory pacientesRepository)
        {
            _pacientesRepository = pacientesRepository;
        }

        // Recebe um paciente e verifica de acordo com as regras de negócio se o cadastro pode ou não ser realizado
        public void CadastrarPaciente(Paciente paciente)
        {
            var existePaciente = ConsultarPacientePorCPF(paciente.Cpf);

            if (existePaciente != null)
            {
                throw new ArgumentException("CPF já cadastrado.");
            }

            if (paciente.GetIdade() < 13)
            {
                throw new ApplicationException($"paciente só tem {paciente.GetIdade()} anos.");
            }
            _pacientesRepository.Salvar(paciente);
        }

        // Recebe um CPF e retorna um paciente, caso caso o mesmo esteja cadastrado na base de pacientes
        public Paciente ConsultarPacientePorCPF(string cpf)
        {
            return _pacientesRepository.ListarTodos()
                                       .Where(p => p.Cpf == cpf)
                                       .SingleOrDefault();
        }

        // Recebe um paciente e verifica de acordo com as regras de negócio se o paciente pode ou não ser excluído
        public void ExcluirPaciente(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ApplicationException("paciente não cadastrado.");
            }

            if (paciente.TemConsultaFutura())
            {
                throw new ApplicationException($"paciente está agendado para {paciente.ConsultaMarcada.Data:dd/MM/yyyy} as {paciente.ConsultaMarcada.GetHorario(paciente.ConsultaMarcada.HoraInicio)}h.");
            }
            _pacientesRepository.Deletar(paciente);
        }

        // Retorna uma lista da base de pacientes ordenada por CPF
        public List<Paciente> ListarPacientesPorCPF()
        {
            return _pacientesRepository.ListarTodos().OrderBy(p => p.Cpf).ToList();
        }

        // Retorna uma lista da base de pacientes ordenada por Nome
        public List<Paciente> ListarPacientesPorNome()
        {
            return _pacientesRepository.ListarTodos().OrderBy(p => p.Nome).ToList();
        }
    }
}
