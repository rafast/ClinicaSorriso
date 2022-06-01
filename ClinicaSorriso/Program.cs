using System;
using ClinicaSorriso.Controllers;
using ClinicaSorriso.Views;
using ClinicaSorriso.Services;
using ClinicaSorriso.Repositories.InMemory;

namespace ClinicaSorriso
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            PacientesRepositoryInMemory pacientesRepositoryInMemory = new PacientesRepositoryInMemory();
            ConsultaRepositoryInMemory consultaRepositoryInMemory = new ConsultaRepositoryInMemory();

            PacienteService pacienteService = new PacienteService(pacientesRepositoryInMemory); 
            ConsultaService consultaService = new ConsultaService(consultaRepositoryInMemory);

            PacienteController pacienteController = new PacienteController(pacienteService, consultaService);
            ConsultaController consultaController = new ConsultaController(consultaService, pacienteService);

            AppController app = new AppController(pacienteController, consultaController);
            MenuView.MenuPrincipal();
            app.LerOpcaoUsuario();
        }
     
    }
}
