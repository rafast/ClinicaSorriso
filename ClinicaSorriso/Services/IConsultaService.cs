using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Services
{
    // Interface de serviço responsável pelas operações da Consulta entre o controller e o repositorio
    public interface IConsultaService
    {
        List<Consulta> ListarConsultas();
        List<Consulta> ListarConsultasPorPeriodo(DateTime dtInicio, DateTime dtFim);
        List<Consulta> ListarConsultasDoDia(DateTime data);
        Consulta BuscarConsulta(Consulta consulta);
        void CadastrarConsulta(Consulta consulta);
        void ExcluirConsulta(Consulta consulta);
        bool TemConflitoDeHorario(Consulta novaConsulta);
        void ExcluirConsultasDoPaciente(Paciente paciente);
    }
}
