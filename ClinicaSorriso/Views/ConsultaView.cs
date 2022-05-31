using ClinicaSorriso.Helpers;
using ClinicaSorriso.Models;
using System;
using System.Collections.Generic;

namespace ClinicaSorriso.Views
{
    public static class ConsultaView
    {
        private static string PacienteCpf { get; set; }
        public static void MenuAgenda()
        {
            Console.WriteLine("Agenda");
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar p/ menu principal");
        }
        public static List<string> Cadastrar()
        {  
            List<string> DadosValidados = new List<string>();

            Console.Write("Data da consulta: ");
            string inputData = Console.ReadLine();    
            Console.Write("Horário do inicio da consulta(HH/mm): ");
            string inputHrInicio = Console.ReadLine();
            Console.Write("Horário do término da consulta(HH/mm): ");
            string inputHrFinal = Console.ReadLine();

            var validadorConsulta = new ValidadorConsulta(inputData, inputHrInicio, inputHrFinal);
            validadorConsulta.ValidarDados();

            while (!validadorConsulta.IsValid())
            {
                validadorConsulta.ExibirErros();

                string novaLeitura = "";

                foreach (var campoInvalido in validadorConsulta.Erros)
                {
                    Console.Write($"{campoInvalido.Key} :");
                    novaLeitura = Console.ReadLine();
                    validadorConsulta.GetType()
                                     .GetProperty(campoInvalido.Key)
                                     .SetValue(validadorConsulta, novaLeitura);
                }
                validadorConsulta.ValidarDados();
            }
            DadosValidados.Add(validadorConsulta.DataConsulta.ToString());
            DadosValidados.Add(validadorConsulta.HoraInicio);
            DadosValidados.Add(validadorConsulta.HoraFim);
            return DadosValidados;
        }

        public static void PacienteInesxistente()
        {
            Console.WriteLine("Erro: paciente não cadastrado ");
        }

        public static string ConsultarCpf()
        {
            Console.Write("CPF: ");
            PacienteCpf = Console.ReadLine();
            return PacienteCpf;
        }
        
        public static char ObterOpcaoListagem()
        {
            Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
            var inputOpcaoListagem = Console.ReadKey();
            return inputOpcaoListagem.KeyChar;
        }

        public static void ListarAgenda(List<Consulta> agenda)
        {
            if (agenda.Count == 0)
            {
                Console.WriteLine("Agenda vazia.");
                return;
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("   Data    H.Ini H.Fim Tempo Nome                   Dt.Nasc.");
            Console.WriteLine("------------------------------------------------------------");
            foreach (var consulta in agenda)
            {
                Console.WriteLine(String.Format("{0,-10} {1,0} {2, 0} {3, 5} {4, -20} {5, 1}", consulta.Data.ToString("dd/MM/yyyy"), consulta.HoraInicio, consulta.HoraFim, consulta.TempoDeConsulta.ToString(@"hh\:mm"),
                                        consulta.Paciente.Nome,
                                        consulta.Paciente.DataNascimento.ToString("dd/MM/yyyy")));
            }
        }
    }
}
