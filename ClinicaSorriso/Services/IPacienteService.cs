using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Services
{
    public interface IPacienteService
    {
        List<Paciente> ListarPacientesPorCPF();
        List<Paciente> ListarPacientesPorNome();
        Paciente ConsultarPacientePorCPF(string cpf);
        void CadastrarPaciente(Paciente paciente);
        void ExcluirPaciente(Paciente paciente);
    }
}
