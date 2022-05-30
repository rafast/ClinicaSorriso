using ClinicaSorriso.Models;
using ClinicaSorriso.Repositories.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaSorriso.Services
{
    public class ConsultaService : IConsultaService
    {
        private ConsultaRepositoryInMemory _consultaRepositoryInMemory { get; set; }

        public ConsultaService(ConsultaRepositoryInMemory consultaRepositoryInMemory)
        {
            _consultaRepositoryInMemory = consultaRepositoryInMemory;
        }

        public void CadastrarConsulta(Consulta consulta)
        {
            if (consulta.Paciente == null)
            {
                throw new ArgumentException("Consulta sem Paciente!!");
            }
            _consultaRepositoryInMemory.Salvar(consulta);

        }

        public void ExcluirConsulta(Consulta consulta)
        {
            throw new NotImplementedException();
        }

        public List<Consulta> ListarConsultas()
        {
            return _consultaRepositoryInMemory.ListarTodos().OrderBy(c => c.Data).ToList();
        }
    }
}