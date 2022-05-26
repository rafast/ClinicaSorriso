using System;
using System.Collections.Generic;

namespace ClinicaSorriso.Repositories
{
    public interface IRepository<T>
    {
        List<T> ListarTodos();
        void Salvar(T entity);
        void Deletar(T entity);
    }
}
