using System.Collections.Generic;

namespace ClinicaSorriso.Repositories
{
    // Interface do repositório de dados
    public interface IRepository<T>
    {
        List<T> ListarTodos();
        void Salvar(T entity);
        void Deletar(T entity);
    }
}
