using System;
using System.Collections.Generic;
using ClinicaSorriso.Helpers;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Views
{
    // Classe responsável por exibir e ler informações referentes ao Paciente
    public static class PacienteView
    {        

        // Exibe o layout do menu do paciente
        public static void MenuPaciente()
        {
            Console.WriteLine("Menu do Cadastro de Pacientes");
            Console.WriteLine("1-Cadastrar novo paciente");
            Console.WriteLine("2-Excluir paciente");
            Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
            Console.WriteLine("4-Listar pacientes (ordenado por nome)");
            Console.WriteLine("5-Voltar p/ menu principal");
        }

        // Obtém um CPF do usuário para consultar se existe na base de pacientes
        public static string ConsultarCpf()
        {
            Console.Write("CPF: ");
            var inputCpf = Console.ReadLine();
            return inputCpf;
        }

        // Faz a leitura e validação dos dados necessários para cadastrar um novo paciente
        public static Paciente Cadastrar()
        {
            Console.Write("CPF: ");
            string inputCpf = Console.ReadLine();
            Console.Write("Nome: ");
            string inputNome = Console.ReadLine();
            Console.Write("Data de nascimento: ");
            string inputDtNascimento = Console.ReadLine();

            var validadorPaciente = new ValidadorPaciente(inputNome, inputCpf, inputDtNascimento);
            validadorPaciente.IniciaValidacao();

            return new Paciente(validadorPaciente.Nome, validadorPaciente.Cpf, Convert.ToDateTime(validadorPaciente.DtNascimento));
        }

        // Exibe mensagem de cadastro realizado com sucesso
        public static void CadastroRealizado()
        {
            Console.WriteLine("Cadastro realizado com sucesso!");
        }

        // Exibe mensagem de cadastro exluido com sucesso
        public static void PacienteExcluido()
        {
            Console.WriteLine("Paciente excluído com sucesso!");
        }

        // Exibe mensagem de erro
        public static void MensagemErro(string msg)
        {
            Console.WriteLine($"Erro: {msg}");
        }

        // Recebe uma lista de pacientes e imprime o layout da listagem de pacientes
        public static void ListarPacientes(List<Paciente> pacientes)
        {
            if(pacientes.Count == 0)
            {
                Console.WriteLine("Lista de pacientes vazia.");
                return;
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("CPF         Nome                              Dt.Nasc. Idade");
            Console.WriteLine("------------------------------------------------------------");
            foreach (var paciente in pacientes)
            {
                Console.WriteLine(String.Format("{0,-11} {1,-32} {2,-10} {3, 1}", paciente.Cpf, paciente.Nome, paciente.DataNascimento.ToString("dd/MM/yyyy"), paciente.GetIdade()));
                if (paciente.TemConsultaFutura())
                {
                    Console.WriteLine(String.Format("{0,-11} {1,-12} ", " ", $"Agendado para: {paciente.ConsultaMarcada.Data.ToString("dd/MM/yyyy")}"));
                    Console.WriteLine(String.Format("{0,-11} {1,-12} ", " ", $"{paciente.ConsultaMarcada.GetHorario(paciente.ConsultaMarcada.HoraInicio)} às {paciente.ConsultaMarcada.GetHorario(paciente.ConsultaMarcada.HoraFim)}"));
                }
            }
        }

        
    }
}
