using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Services
{
    // Interface de serviço responsável pelas operações da Consulta entre o controller e o repositorio
    public interface IConsultaService
    {
        List<Consulta> ListarConsultas();
        void CadastrarConsulta(Consulta consulta);
        void ExcluirConsulta(Consulta consulta, List<string> listaDeDados);
    }
}
