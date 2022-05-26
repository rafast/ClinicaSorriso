using System;
namespace ClinicaSorriso.Views
{
    public static class ConsultaView
    {
        public static void MenuAgenda()
        {
            Console.WriteLine("Agenda");
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar p/ menu principal");
        }
    }
}
