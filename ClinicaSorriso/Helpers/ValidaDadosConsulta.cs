using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClinicaSorriso.Helpers
{
    //Implementacao da interface de validacao para criar e excluir uma consulta
    public class ValidaDadosConsulta : IValidador
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
            ValidarDados();
        }

        //Inicializa o dicionario de erros
        public void InicializaDicionarioDeErros()
        {
            Erros = new Dictionary<string, List<string>>
            {
                { "DataConsulta", new List<string>() },
                { "HoraInicio", new List<string>() },
                { "HoraFim", new List<string>() }
            };
        }

        //Retorna se há algum campo inválido
        public bool HasErrors()
        {
            return Erros.Values.Where(listaErros => listaErros.Count > 0).Any();
        }

        //Realiza a validação dos campos
        public void ValidarDados()
        {
            InicializaDicionarioDeErros();

            ValidaDataConsulta();

            ValidaHoraInicio();

            ValidaHoraFim();
        }

        //Verifica se a data está no formato válido
        private bool IsFormatoDataValido(string data, out DateTime dataValida)
        {
            var tamanhoValido = data.Length == 10;
            var formatoValido = DateTime.TryParseExact(data,
                                         "dd/MM/yyyy",
                                         CultureInfo.InvariantCulture,
                                         DateTimeStyles.None,
                                         out dataValida);

            return tamanhoValido & formatoValido;
        }

        //Verifica se o horario está no formato válido
        private bool IsFormatoHorarioValido(string horario)
        {
            string padrao = "^(0[8,9]|1[0-8])([0,3]0|[1,4]5)$";
            return Regex.IsMatch(horario, padrao);
        }

        //Imprime os campos inválidos e os seus respectivos erros
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

        //Verifica se o horario final é posterior ao inicial
        public bool IsHoraFimPosteriorHoraInicio()
        {
            int.TryParse(HoraFim, out int intHoraFim);
            int.TryParse(HoraInicio, out int intHoraInicio);
            return intHoraFim > intHoraInicio;
        }

        //Verifica se a hora inicial é posterior à hora atual do agendamento
        public bool IsHoraInicioPosteriorHoraAtual()
        {
            if(HoraInicio.Length == 4)
            {
                int.TryParse(HoraInicio.Substring(0, 2), out int intHoras);
                int.TryParse(HoraInicio.Substring(2), out int intMinutos);
                var horaInicioTimeSpan = new TimeSpan(intHoras, intMinutos, 0);
                var horaAtualTimeSpan = DateTime.Now.TimeOfDay;
                return horaInicioTimeSpan > horaAtualTimeSpan;
            }
            return false;
        }

        //Retorna se uma data é futura ou não
        private bool IsDataFutura(DateTime data)
        {
            return data.Date >= DateTime.Now.Date;
        }

        //Retorna se a consulta está sendo agendada pro mesmo dia
        private bool IsDataIgualHoje()
        {
            DateTime data;
            bool dataConvertida = DateTime.TryParseExact(DataConsulta,"dd/MM/yyyy",
                                         CultureInfo.InvariantCulture,
                                         DateTimeStyles.None, out data);
            if (dataConvertida)
            {
                var dataAgendamento = DateTime.Parse(DataConsulta);
                return dataAgendamento.Date == DateTime.Now.Date;
            }
            return dataConvertida;
            
        }

        //Executa todas as validações para a data da consulta
        private void ValidaDataConsulta()
        {
            if (!IsFormatoDataValido(DataConsulta, out DateTime dataValida))
            {
                Erros["DataConsulta"].Add("Data inválida. Deve ser informada no formato DD/MM/AAAA");
            }

            if (!IsDataFutura(dataValida))
            {
                Erros["DataConsulta"].Add("A data da consulta deve ser posterior ou igual a data de hoje");
            }
        }

        //Executa todas as validações para a hora inicial
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

        //Executa todas as validações para a hora final
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

        //Inicia o processo leitura e validação dos campos que estão inválidos
        public void IniciaValidacao()
        {
            while (HasErrors())
            {
                ExibirErros();

                string novaLeitura = "";

                foreach (var campoInvalido in Erros.Where(erro => erro.Value.Count > 0))
                {
                    Console.Write($"{campoInvalido.Key} :");
                    novaLeitura = Console.ReadLine();
                    GetType()
                            .GetProperty(campoInvalido.Key)
                            .SetValue(this, novaLeitura);
                }
                ValidarDados();
            }
        }
    }
}
