using System;
using System.Collections.Generic;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Services
{
    // Interface de serviço responsável pelas operações do Paciente entre o controller e o repositorio
    public interface IPacienteService
    {
        List<Paciente> ListarPacientesPorCPF();
        List<Paciente> ListarPacientesPorNome();
        Paciente ConsultarPacientePorCPF(string cpf);
        void CadastrarPaciente(Paciente paciente);
        void ExcluirPaciente(Paciente paciente);
    }
}
