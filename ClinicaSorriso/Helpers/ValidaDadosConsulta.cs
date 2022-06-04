using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClinicaSorriso.Helpers
{
    public class ValidaDadosConsulta
    {
        public string DataConsulta { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public Dictionary<string, List<string>> Erros { get; set; }

        public ValidaDadosConsulta(string dataConsulta, string horaInicio, string horaFim)
        {
            DataConsulta = dataConsulta;
            HoraInicio = horaInicio;
            HoraFim = horaFim;            
        }

        private void InicializaDicionarioDeErros()
        {
            Erros = new Dictionary<string, List<string>>
            {
                { "DataConsulta", new List<string>() },
                { "HoraInicio", new List<string>() },
                { "HoraFim", new List<string>() }
            };
        }

        public bool HasErrors()
        {
            return Erros.Values.Where(listaErros => listaErros.Count > 0).Any();
        }

        public void ValidarDados()
        {
            InicializaDicionarioDeErros();

            ValidaDataConsulta();

            ValidaHoraInicio();

            ValidaHoraFim();
        }

        private bool IsFormatoDataValido(out DateTime data)
        {
            return DateTime.TryParseExact(DataConsulta,
                                         "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture,
                                         DateTimeStyles.None,
                                         out data);            
        }

        private bool IsFormatoHorarioValido(string horario)
        {
            string padrao = "^(0[8,9]|1[0-8])([0,3]0|[1,4]5)$";
            return Regex.IsMatch(horario, padrao);
        }

        public void ExibirErros()
        {
            foreach (var campo in Erros)
            {
                if (campo.Value.Count > 0)
                {
                    foreach (var erro in campo.Value)
                    {
                        Console.WriteLine($"Erro {campo.Key}: {erro}");
                    }
                }
            }
        }

        public bool IsHoraFimPosteriorHoraInicio()
        {
            int.TryParse(HoraFim, out int intHoraFim);
            int.TryParse(HoraInicio, out int intHoraInicio);
            return intHoraFim > intHoraInicio;
        }

        public bool IsHoraInicioPosteriorHoraAtual()
        {
            int.TryParse(HoraInicio.Substring(0,2), out int intHoras);
            int.TryParse(HoraInicio.Substring(2), out int intMinutos);
            var horaInicioTimeSpan = new TimeSpan(intHoras, intMinutos, 0);
            var horaAtualTimeSpan = DateTime.Now.TimeOfDay;
            return horaInicioTimeSpan > horaAtualTimeSpan;
        }

        private bool IsDataFutura(DateTime data)
        {
            return data.Date >= DateTime.Now.Date;
        }

        private bool IsDataIgualHoje()
        {
            var dataAgendamento = DateTime.Parse(DataConsulta);
            return dataAgendamento.Date == DateTime.Now.Date;
        }

        private void ValidaDataConsulta()
        {
            if (!IsFormatoDataValido(out DateTime dataValida))
            {
                Erros["DataConsulta"].Add("Data inválida. Deve ser informada no formato DD/MM/AAAA");
            }

            if (!IsDataFutura(dataValida))
            {
                Erros["DataConsulta"].Add("A data da consulta deve ser posterior ou igual a data de hoje");
            }
        }

        private void ValidaHoraInicio()
        {
            if (!IsFormatoHorarioValido(HoraInicio))
            {
                Erros["HoraInicio"].Add("As horas devem estar no formato HHMM. Dentro do horário de funcionamento (08:00h às 19:00h). De 15 em 15 minutos.");
            }

            if (IsDataIgualHoje() & !IsHoraInicioPosteriorHoraAtual())
            {
                Erros["HoraInicio"].Add($"A hora inicial deve ser posterior a hora atual {DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss")}");
            }


        }

        private void ValidaHoraFim()
        {
            if (!IsFormatoHorarioValido(HoraFim))
            {
                if (!HoraFim.Equals("1900"))
                {
                    Erros["HoraFim"].Add("As horas devem estar no formato HHMM. Dentro do horário de funcionamento (08:00h às 19:00h). De 15 em 15 minutos.");
                }                    
            }

            if (!IsHoraFimPosteriorHoraInicio())
            {
                Erros["HoraFim"].Add("A hora final deve ser posterior a hora inicial.");
            }
        }

    }
}
