using ClinicaSorriso.Helpers;
using ClinicaSorriso.Models;
using System;
using System.Collections.Generic;

namespace ClinicaSorriso.Views
{
    // Classe responsável por exibir e ler informações referentes as consultas
    public static class ConsultaView
    {
        // Exibe o layout do menu da agenda
        public static void MenuAgenda()
        {
            Console.WriteLine("Agenda");
            Console.WriteLine("1-Agendar consulta");
            Console.WriteLine("2-Cancelar agendamento");
            Console.WriteLine("3-Listar agenda");
            Console.WriteLine("4-Voltar p/ menu principal");
        }

        // Faz a leitura e validação dos dados necessários para agendar uma consulta
        public static ValidaDadosConsulta ObterDadosConsulta()
        {  
            Console.Write("Data da consulta: ");
            string inputData = Console.ReadLine();    
            Console.Write("Hora inicial: ");
            string inputHrInicio = Console.ReadLine();
            Console.Write("Hora final: ");
            string inputHrFinal = Console.ReadLine();


            var validadorConsulta = new ValidaDadosConsulta(inputData, inputHrInicio, inputHrFinal);
            validadorConsulta.IniciaValidacao();          

            return validadorConsulta;
        }

        // Exibe mensagem de consulta agendada com sucesso
        public static void AgendamentoRealizado()
        {
            Console.WriteLine("Agendamento realizado com sucesso!");
        }

        // Exibe mensagem de consulta excluída com sucesso
        public static void ConsultaExcluida()
        {
            Console.WriteLine("Consulta excluída com sucesso!");
        }

        // Exibe mensagem de erro
        public static void MensagemErro(string msg)
        {
            Console.WriteLine($"Erro: {msg}");
        }

        //Faz a leitura da opcao de listagem da agenda
        public static char ObterOpcaoListagem()
        {
            Console.Write("Apresentar a agenda T-Toda ou P-Periodo: ");
            var inputOpcaoListagem = Console.ReadKey();
            return inputOpcaoListagem.KeyChar;
        }

        //Faz a leitura e validacao das datas para listagem da agenda por periodo
        public static DateTime[] ObterPeriodoListagem()
        {
            Console.Write("Data inicial: ");
            string inputDtInicio = Console.ReadLine();
            Console.Write("Data final: ");
            string inputDtFinal = Console.ReadLine();

            var ValidadorDatas = new ValidadorDatas(inputDtInicio, inputDtFinal);

            ValidadorDatas.IniciaValidacao();

            var datasPeriodo = new DateTime[]
            {
                DateTime.Parse(ValidadorDatas.DataInicio),
                DateTime.Parse(ValidadorDatas.DataFim)
            };

            return datasPeriodo;
        }

        // Recebe uma lista de consultas agendadas e imprime o layout da listagem da agenda
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
                Console.WriteLine(String.Format("{0,-10} {1,0} {2, 0} {3, 5} {4, -20} {5, 1}", consulta.Data.ToString("dd/MM/yyyy"), consulta.GetHorario(consulta.HoraInicio), consulta.GetHorario(consulta.HoraFim), consulta.TempoDeConsulta.ToString(@"hh\:mm"),
                                        consulta.Paciente.Nome,
                                        consulta.Paciente.DataNascimento.ToString("dd/MM/yyyy")));
            }
        }
    }
}
