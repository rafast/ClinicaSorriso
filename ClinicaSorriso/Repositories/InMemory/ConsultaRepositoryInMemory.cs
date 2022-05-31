using System.Collections.Generic;
using ClinicaSorriso.Models;


namespace ClinicaSorriso.Repositories.InMemory
{
    public class ConsultaRepositoryInMemory : IRepository<Consulta>
    {
        private List<Consulta> _consultaRepository { get; set; }

        public ConsultaRepositoryInMemory()
        {
            _consultaRepository = new List<Consulta>();
        }

        public void Salvar(Consulta entity)
        {
            _consultaRepository.Add(entity);
        }

        public void Deletar(Consulta entity)
        {
            _consultaRepository.Remove(entity);
        }

        public List<Consulta> ListarTodos()
        {
            return _consultaRepository;
        }
        
    }
}