using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Repositories.InMemory
{
    // Implementação da interface do repositório de Pacientes em memória
    public class PacientesRepositoryInMemory : IRepository<Paciente>
    {
        private List<Paciente> _pacientesRepository { get; set; }

        public PacientesRepositoryInMemory()
        {
            _pacientesRepository = new List<Paciente>();
            PopulaPacientes();
        }

        // Recebe um Paciente e o exclui da base de pacientes
        public void Deletar(Paciente entity)
        {
            _pacientesRepository.Remove(entity);
        }

        // Retorna uma lista com tdos os pacientes da base de pacientes
        public List<Paciente> ListarTodos()
        {
            return _pacientesRepository;
        }

        // Recebe um paciente e salva na base de pacientes
        public void Salvar(Paciente entity)
        {
            _pacientesRepository.Add(entity);
        }

        // Cria alguns pacientes na base de pacientes inicial
        private void PopulaPacientes()
        {
            _pacientesRepository.Add(new Paciente("Joao Alves", "39401787050", Convert.ToDateTime("13/09/1982")));
            _pacientesRepository.Add(new Paciente("Paula Silva", "80519936086", Convert.ToDateTime("24/02/1981")));
            _pacientesRepository.Add(new Paciente("Mateus Pereira", "61461811023", Convert.ToDateTime("12/05/1978")));
            _pacientesRepository.Add(new Paciente("Wagner Peixoto", "91490575022", Convert.ToDateTime("22/02/1968")));
            _pacientesRepository.Add(new Paciente("Ana Beatriz", "34645846078", Convert.ToDateTime("28/10/2000")));
            _pacientesRepository.Add(new Paciente("Andre Rocha", "69024641039", Convert.ToDateTime("07/05/1984")));
        }
    }
}
