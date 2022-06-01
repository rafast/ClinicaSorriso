using System;
using ClinicaSorriso.Helpers;
using ClinicaSorriso.Services;
using ClinicaSorriso.Views;

namespace ClinicaSorriso.Controllers
{
    public class PacienteController
    {
        private PacienteService _pacienteService { get; set; }
        private ConsultaService _consultaService { get; set; }

        public PacienteController(PacienteService pacienteService, ConsultaService consultaService)
        {
            _pacienteService = pacienteService;
            _consultaService = consultaService;
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
                    Console.Clear();
                    Excluir();
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
            catch (ApplicationException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        public void Excluir()
        {
            var pacienteSalvo = _pacienteService.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
            if (pacienteSalvo == null)
            {
                PacienteView.PacienteInesxistente();
                return;
            }
            try
            {
                _pacienteService.ExcluirPaciente(pacienteSalvo);                
                _consultaService.ExcluirConsultasDoPaciente(pacienteSalvo);
                Console.WriteLine("Paciente excluído com sucesso!");
            }
            catch (ApplicationException)
            {
                Console.WriteLine($"Erro: paciente está agendado para {pacienteSalvo.ConsultaMarcada.Data.ToString("dd/MM/yyyy")} as {pacienteSalvo.ConsultaMarcada.HoraInicio}h");
            }
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
