using System;
using ClinicaSorriso.Helpers;
using ClinicaSorriso.Services;
using ClinicaSorriso.Views;

namespace ClinicaSorriso.Controllers
{
    public class PacienteController
    {
        private PacienteService _pacienteService { get; set; }

        public PacienteController(PacienteService pacienteService)
        {
            this._pacienteService = pacienteService;
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
                    Console.Clear();
                    Cadastrar();
                    break;
                case '2':
                    Console.WriteLine("Excluir paciente");
                    //Excluir();
                    break;
                case '3':
                    Console.Clear();
                    ListarPorCpf();
                    break;
                case '4':
                    Console.Clear();
                    ListarPorNome();
                    break;
                case '5':                                               
                    Console.WriteLine("Voltar p/ menu principal");
                    Console.Clear();
                    MenuView.MenuPrincipal();
                    exit = true;
                    break;
                default:
                    Console.Clear();
                    PacienteView.MenuPaciente();
                    break;
                }

            }

        }

        public void Cadastrar()
        {
            try
            {
                _pacienteService.CadastrarPaciente(PacienteView.Cadastrar());
                Console.WriteLine("Cadastro realizado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        public void Excluir()
        {

        }

        public void ListarPorCpf()
        {
            PacienteView.ListarPacientes(_pacienteService.ListarPacientesPorCPF()); 
        }

        public void ListarPorNome()
        {
            PacienteView.ListarPacientes(_pacienteService.ListarPacientesPorNome());
        }
    }
}
