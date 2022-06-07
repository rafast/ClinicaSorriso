using System.Collections.Generic;
using ClinicaSorriso.Models;


namespace ClinicaSorriso.Repositories.InMemory
{
    // Implementação da interface do repositório de Consultas em memória
    public class ConsultaRepositoryInMemory : IRepository<Consulta>
    {
        private List<Consulta> _consultaRepository { get; set; }

        public ConsultaRepositoryInMemory()
        {
            _consultaRepository = new List<Consulta>();
        }

        // Recebe uma Consulta e salva na base de consultas
        public void Salvar(Consulta entity)
        {
            _consultaRepository.Add(entity);
        }

        // Recebe uma Consulta e exclui da base de consultas
        public void Deletar(Consulta entity)
        {
            _consultaRepository.Remove(entity);
        }

        // Retorna uma lista com todas as consultas da base de consultas
        public List<Consulta> ListarTodos()
        {
            return _consultaRepository;
        }
        
    }
}