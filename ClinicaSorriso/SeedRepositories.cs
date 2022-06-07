using System;
using ClinicaSorriso.Models;
using ClinicaSorriso.Services;

namespace ClinicaSorriso
{
    //Classe responsável por criar alguns pacientes e consultas para testar a aplicação
    public class SeedRepositories
    {
        private PacienteService _pacienteService { get; set; }
        private ConsultaService _consultaService { get; set; }

        public SeedRepositories(PacienteService pacienteService, ConsultaService consultaService)
        {
            _pacienteService = pacienteService;
            _consultaService = consultaService;
        }

        //Criando pacientes e consultas
        public void PopularBanco()
        {
            var joao = new Paciente("Joao Alves", "39401787050", Convert.ToDateTime("13/09/1982"));
            var paula = new Paciente("Paula Silva", "80519936086", Convert.ToDateTime("24/02/1981"));
            var mateus = new Paciente("Mateus Pereira", "61461811023", Convert.ToDateTime("12/05/1978"));
            var wagner = new Paciente("Wagner Peixoto", "91490575022", Convert.ToDateTime("22/02/1968"));
            var ana = new Paciente("Ana Beatriz", "34645846078", Convert.ToDateTime("28/10/2000"));
            var andre = new Paciente("Andre Rocha", "69024641039", Convert.ToDateTime("07/05/1984"));

            var consultaJoao = new Consulta(joao, Convert.ToDateTime("15/05/2022"), "0800", "0900");
            var consultaPaula = new Consulta(paula, Convert.ToDateTime("15/06/2022"), "1015", "1100");
            var consultaWagner = new Consulta(wagner, Convert.ToDateTime("15/06/2022"), "1345", "1500");

            _pacienteService.CadastrarPaciente(joao);
            _pacienteService.CadastrarPaciente(paula);
            _pacienteService.CadastrarPaciente(mateus);
            _pacienteService.CadastrarPaciente(wagner);
            _pacienteService.CadastrarPaciente(ana);
            _pacienteService.CadastrarPaciente(andre);

            _consultaService.CadastrarConsulta(consultaJoao);
            _consultaService.CadastrarConsulta(consultaPaula);
            _consultaService.CadastrarConsulta(consultaWagner);

            joao.MarcarConsulta(consultaJoao);
            paula.MarcarConsulta(consultaPaula);
            wagner.MarcarConsulta(consultaWagner);
        }
    }
}
