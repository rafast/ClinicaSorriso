using ClinicaSorriso.Models;
using ClinicaSorriso.Repositories.InMemory;
using ClinicaSorriso.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClinicaSorriso.Helpers
{
    public class ValidadorConsulta
    {
        public PacientesRepositoryInMemory Prepo { get; set; }
        public Dictionary<string, string> Erros { get; set; }
        public Paciente Paciente { get; set; }
        public string Data { get; set; }
        public string HoraInicio { get; set; }
        public string Cpf { get; set; }

        public string HoraFim { get; set; }
        public DateTime DataConsulta { get; set; }
        private double _horarioDeAbertura = 8.00;
        private double _horarioDeEncerramento = 19.00;

        public ValidadorConsulta(string data, string horaInicio, string horaFim)
        {

            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Erros = new Dictionary<string, string>();
        }

        public bool IsValid()
        {
            return Erros.Count == 0;
        }

        public void ValidarDados()
        {
            Erros.Clear();
            /* if (!VinculaPaciente(Cpf))
             {
                 Erros.Add("Cpf", "cpf do paciente não esta cadastrado");
             }*/
            if (!ValidaData())
            {
                Erros.Add("Data", "Data inválida");
            }

            if (!ValidaHorarios(HoraInicio))
            {
                Erros.Add("HoraInicio", "Digite um horário válido");
            }

            if (!ValidaHorarios(HoraFim))
            {
                Erros.Add("HoraFim", "Digite um horário válido");
            }
        }

        /*private bool VinculaPaciente(string cpf)
        {
            PacientesRepositoryInMemory Rp = Prepo;
            Paciente pac = Rp.ListarTodos()
                             .Where(p => p.Cpf == cpf)
                             .SingleOrDefault();
            if (pac == null)
            {
                return false;
            }
            else
            {
                Paciente = pac;
                return true;
            }
        }*/

        public void ExibirErros()
        {
            foreach (var erro in Erros)
            {
                Console.WriteLine($"Erro: {erro.Value}");
            }
        }
        private bool VerificaHorario(string horario)
        {
            string padrao = "[0-1][0-9][0-5][0-9]";

            if (Regex.IsMatch(horario, padrao))
            {
                string minuto = horario.Substring(2, 2);
                if (minuto == "00" || minuto == "15" || minuto == "30" || minuto == "45")
                {
                    if (VerificaDisponibilidade(horario))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ValidaHorarios(string horario)
        {
            if (horario == HoraInicio || horario == HoraFim)
            {
                string hInicial = HoraInicio.Replace(":", "");
                string hFinal = HoraFim.Replace(":", "");
                if (VerificaHorario(hInicial) && VerificaHorario(hFinal))
                {
                    InicializaDataConsulta();
                    return double.Parse(hInicial) < double.Parse(hFinal);
                }
            }
            return false;
        }
        private bool ValidaData()
        {
            InicializaDataConsulta();
            if (DataConsulta >= DateTime.Now)
            {
                return true;
            }
            else if (DataConsulta < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        private bool VerificaDisponibilidade(string horario)
        {
            string hora = horario.Substring(0, 2);
            int iHora = int.Parse(hora);

            if (iHora >= _horarioDeAbertura)
            {
                if (iHora < _horarioDeEncerramento)
                {
                    return true;
                }
                else if (iHora == _horarioDeEncerramento)
                {
                    string minuto = horario.Substring(2, 2);
                    int iMinuto = int.Parse(minuto);
                    return iMinuto == 0;
                }
            }
            return false;
        }
        private void InicializaDataConsulta()
        {
            if (VerificaFormatoData(Data, out DateTime result))
            {
                DataConsulta = result;

                HoraInicio = HoraInicio.Replace(":", "");
                if (VerificaHorario(HoraInicio))
                {
                    string hora = HoraInicio.Substring(0, 2);
                    string minuto = HoraInicio.Substring(2, 2);
                    DateTime d2 = DataConsulta.AddHours(double.Parse(hora)).AddMinutes(double.Parse(minuto));
                    DataConsulta = d2;
                }
            }
        }
        private bool VerificaFormatoData(string value, out DateTime data)
        {
            string dt = Data;

            return DateTime.TryParseExact(dt,
                                         "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture,
                                         DateTimeStyles.None,
                                          out data);
        }
    }
}