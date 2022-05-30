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
            Console.WriteLine("CPF: ");
           PacienteCpf = Console.ReadLine();
            return PacienteCpf;
        }
        
        public static void ListarPacientes(List<Paciente> pacientes)
        {
            if (pacientes.Count == 0)
            {
                Console.WriteLine("Lista de pacientes vazia.");
                return;
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Data         Nome                              Dt.Nasc. Idade");
            Console.WriteLine("------------------------------------------------------------");
            foreach (var paciente in pacientes)
            {
                Console.WriteLine(String.Format("{0,-11} {1,-32} {2,-10} {3, 1}", paciente.Cpf, paciente.Nome, paciente.DataNascimento.ToString("dd/MM/yyyy"), paciente.GetIdade()));
            }
        }
    }
}
