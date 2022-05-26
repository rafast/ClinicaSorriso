using System;
using System.Collections.Generic;
using ClinicaSorriso.Controllers;
using ClinicaSorriso.Helpers;
using ClinicaSorriso.Models;

namespace ClinicaSorriso.Views
{
    public static class PacienteView
    {        

        public static void MenuPaciente()
        {
            Console.WriteLine("Menu do Cadastro de Pacientes");
            Console.WriteLine("1-Cadastrar novo paciente");
            Console.WriteLine("2-Excluir paciente");
            Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
            Console.WriteLine("4-Listar pacientes (ordenado por nome)");
            Console.WriteLine("5-Voltar p/ menu principal");
        }

        public static Paciente Cadastrar()
        {
            Console.Write("CPF: ");
            string inputCpf = Console.ReadLine();
            Console.Write("Nome: ");
            string inputNome = Console.ReadLine();
            Console.Write("Data de nascimento: ");
            string inputDtNascimento = Console.ReadLine();

            var validadorPaciente = new ValidadorPaciente(inputNome, inputCpf, inputDtNascimento);
            validadorPaciente.ValidarDados();

            while (!validadorPaciente.IsValid())
            {
                validadorPaciente.ExibirErros();

                string novaLeitura = "";

                foreach (var campoInvalido in validadorPaciente.erros)
                {
                    Console.Write($"{campoInvalido.Key} :");
                    novaLeitura = Console.ReadLine();
                    validadorPaciente.GetType()
                                     .GetProperty(campoInvalido.Key)
                                     .SetValue(validadorPaciente, novaLeitura);
                }
                validadorPaciente.ValidarDados();
            }

            return new Paciente(validadorPaciente.Nome, validadorPaciente.Cpf, Convert.ToDateTime(validadorPaciente.DtNascimento));
        }

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
            }
        }

        
    }
}
