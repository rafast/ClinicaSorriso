using System;
using ClinicaSorriso.Views;

namespace ClinicaSorriso.Controllers
{
    //Classe de controle da excecução da aplicação
    public class AppController
    {
        public PacienteController pacienteController { get; set; }
        public ConsultaController consultaController { get; set; }

        public AppController(PacienteController pacienteController, ConsultaController consultaController)
        {
            this.pacienteController = pacienteController;
            this.consultaController = consultaController;
        }

        public void LerOpcaoUsuario()
        {
            bool exit = false;

            while (!exit)
            {
                var opcao = Console.ReadKey();

                switch (opcao.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        PacienteView.MenuPaciente();
                        pacienteController.LeituraOpcao();
                        break;
                    case '2':
                        Console.Clear();
                        ConsultaView.MenuAgenda();
                        consultaController.LeituraOpcao();
                        break;
                    case '3':
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        MenuView.MenuPrincipal();
                        break;
                }

            }
        }

        public void Run()
        {
            MenuView.MenuPrincipal();
            LerOpcaoUsuario();
        }
        
    }
}
