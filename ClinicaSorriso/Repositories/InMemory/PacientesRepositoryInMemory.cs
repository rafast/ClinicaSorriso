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
        }

        // Recebe um Paciente e o exclui da base de pacientes
        public void Deletar(Paciente entity)
        {
            _pacientesRepository.Remove(entity);
        }

        // Retorna uma lista com dos os pacientes da base de pacientes
        public List<Paciente> ListarTodos()
        {
            return _pacientesRepository;
        }

        // Recebe um paciente e salva na base de pacientes
        public void Salvar(Paciente entity)
        {
            _pacientesRepository.Add(entity);
        }

    }
}
