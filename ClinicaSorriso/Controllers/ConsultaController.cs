using System;
using System.Linq;
using ClinicaSorriso.Models;
using ClinicaSorriso.Services;
using ClinicaSorriso.Views;

namespace ClinicaSorriso.Controllers
{
    public class ConsultaController
    {
        private PacienteService _pacienteService { get; set; }
        private ConsultaService _consultaService { get; set; }

        public ConsultaController(ConsultaService consultaService, PacienteService pacienteService)
        {
            _consultaService = consultaService;
            _pacienteService = pacienteService;
            PopularAgenda();
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
                        ListarAgenda();
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
        public void Cadastrar()
        {
            try
            {
                var pacienteSalvo = _pacienteService.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
                if (pacienteSalvo == null)
                {
                    PacienteView.PacienteInesxistente();
                    return;
                }
                else if(pacienteSalvo.ConsultaMarcada != null)
                {
                    Console.WriteLine($"Erro: o paciente já possui consulta marcada para:" +
                        $"{pacienteSalvo.ConsultaMarcada.Data.ToString("dd/MM/yyyy")}");
                    return;
                }
                var dadosConsulta = ConsultaView.Cadastrar();
                var novaConsulta = new Consulta(pacienteSalvo, DateTime.Parse(dadosConsulta[0]), dadosConsulta[1], dadosConsulta[2]);
                var temChoqueDehorario = _consultaService.TemChoqueDeHorario(novaConsulta);
                if (temChoqueDehorario)
                {
                    Console.WriteLine("Erro: já existe uma consulta agendada nesta data/hora ");
                    return;
                }
                _consultaService.CadastrarConsulta(novaConsulta);
                pacienteSalvo.MarcarConsulta(novaConsulta);
                Console.WriteLine("Agendamento realizado com sucesso!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        public void Excluir()
        {
            var pacienteConsulta = _pacienteService.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
            if (pacienteConsulta == null)
            {
                PacienteView.PacienteInesxistente();
                return;
            }
            if (!pacienteConsulta.TemConsultaFutura())
            {
                Console.WriteLine("O paciente não possui consulta marcada!");
                return;
            }
            var consulta = pacienteConsulta.ConsultaMarcada;
            var listaDeDados = ConsultaView.Excluir();
            try
            {
                _consultaService.ExcluirConsulta(consulta, listaDeDados);
                pacienteConsulta.CancelarConsulta();
                Console.WriteLine("Consulta excluída com sucesso!");
            }
            catch (ApplicationException e)
            {
                Console.WriteLine($"Error: {e.Message} ");
            }
        }

        public void ListarAgenda()
        {
            var opcaoListagem = ConsultaView.ObterOpcaoListagem();
            Console.Clear();
            if (opcaoListagem == 'T' || opcaoListagem == 't')
            {
                ConsultaView.ListarAgenda(_consultaService.ListarConsultas());
            }
            if (opcaoListagem == 'P')
            {

            }
            else
            {
                Console.WriteLine();
            }
        }

        private void PopularAgenda()
        {
            _consultaService.CadastrarConsulta(new Consulta(_pacienteService.ConsultarPacientePorCPF("39401787050"), Convert.ToDateTime("15/05/2022 "), "08:00", "09:00"));
            _consultaService.CadastrarConsulta(new Consulta(_pacienteService.ConsultarPacientePorCPF("80519936086"), Convert.ToDateTime("15/06/2022"), "10:15", "11:00"));
            _consultaService.CadastrarConsulta(new Consulta(_pacienteService.ConsultarPacientePorCPF("91490575022"), Convert.ToDateTime("15/06/2022"), "13:45", "15:00"));
        }

    }
}
