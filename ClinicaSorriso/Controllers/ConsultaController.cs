using System;
using ClinicaSorriso.Views;

namespace ClinicaSorriso.Controllers
{
    public class ConsultaController
    {
        public ConsultaController()
        {
        }

        public void LeituraOpcao()
        {
            bool exit = false;

            while (!exit)
            {
                var opcao = Console.ReadKey();
                switch (opcao.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Agendar consulta");
                        //Cadastrar();
                        break;
                    case '2':
                        Console.WriteLine("Cancelar agendamento");
                        //Excluir();
                        break;
                    case '3':
                        Console.WriteLine("Listar agenda");
                        //ListarPorCpf();
                        break;
                    case '4':
                        Console.WriteLine("Voltar p/ menu principal");
                        Console.Clear();
                        MenuView.MenuPrincipal();
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        ConsultaView.MenuAgenda();
                        break;
                }

            }

        }
    }
}
