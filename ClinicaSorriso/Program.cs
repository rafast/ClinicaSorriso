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
            //var pacienteController = new PacienteController();
            //pacienteController.Cadastrar();
            PacientesRepositoryInMemory pacientesRepositoryInMemory = new PacientesRepositoryInMemory();
            PacienteService pacienteService = new PacienteService(pacientesRepositoryInMemory); 
            PacienteController pacienteController = new PacienteController(pacienteService);
            AppController app = new AppController(pacienteController);
            MenuView.MenuPrincipal();
            app.LerOpcaoUsuario();
        }
     
    }
}
