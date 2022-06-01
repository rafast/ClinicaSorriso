using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Services
{
    public interface IConsultaService
    {
        List<Consulta> ListarConsultas();
        void CadastrarConsulta(Consulta consulta);
        void ExcluirConsulta(Consulta consulta, List<string> listaDeDados);
    }
}
