using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ClinicaSorriso.Helpers
{
    //Implementacao da interface de validacao para data inicial e final do periodo de listagem da agenda
    public class ValidadorDatas : IValidador
    {

        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public Dictionary<string, List<string>> Erros { get; set; }

        public ValidadorDatas(string dataInicio, string dataFim)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            ValidarDados();
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

        //Inicializa o dicionario de erros
        public void InicializaDicionarioDeErros()
        {
            Erros = new Dictionary<string, List<string>>
            {
                { "DataInicio", new List<string>() },
                { "DataFim", new List<string>() }
            };
        }

        //Retorna se há alguma campo invalido
        public bool HasErrors()
        {
            return Erros.Values.Where(listaErros => listaErros.Count > 0).Any();
        }

        // Imprime o campo inválido e o seus respectivos erros
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

        //Executa a validacao dos campos de acordo com as regras de negócio da aplicação
        public void ValidarDados()
        {
            InicializaDicionarioDeErros();

            ValidaDataInicio();

            ValidaDataFim();

        }

        //Verifica se a data está no formato válido
        private void ValidaDataInicio()
        {
            if (!IsFormatoDataValido(DataInicio, out DateTime dataValida))
            {
                Erros["DataInicio"].Add("Data inválida. Deve ser informada no formato DD/MM/AAAA.");
            }
        }

        //Verifica se a data está no formato válido e é posterior à data inicial
        private void ValidaDataFim()
        {
            if (!IsFormatoDataValido(DataFim, out DateTime dataValida))
            {
                Erros["DataFim"].Add("Data inválida. Deve ser informada no formato DD/MM/AAAA.");
            }

            if (!IsDataPosterior(DataInicio, DataFim))
            {
                Erros["DataFim"].Add("A data final deve ser posterior a data inicial.");
            }
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
             
            return tamanhoValido && formatoValido;
            
        }

        //Verifica se a data é posterior
        public bool IsDataPosterior(string dataInicio, string dataFim)
        {
            if (IsFormatoDataValido(dataInicio, out DateTime dtInicioValida) & IsFormatoDataValido(dataFim, out DateTime dtFimValida))
            {
                var dtInicio = DateTime.Parse(dataInicio);
                var dtFim = DateTime.Parse(dataFim);
                return dtFim.Date > dtInicio.Date;
            }
            return false;
        }

    }
}
