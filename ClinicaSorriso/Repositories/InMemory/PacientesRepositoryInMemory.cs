using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Repositories.InMemory
{
    public class PacientesRepositoryInMemory : IRepository<Paciente>
    {
        private List<Paciente> _pacientesRepository { get; set; }

        public PacientesRepositoryInMemory()
        {
            _pacientesRepository = new List<Paciente>();
            PopulaPacientes();
        }

        public void Deletar(Paciente entity)
        {
            _pacientesRepository.Remove(entity);
        }

        public List<Paciente> ListarTodos()
        {
            return _pacientesRepository;
        }

        public void Salvar(Paciente entity)
        {
            _pacientesRepository.Add(entity);
        }

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
