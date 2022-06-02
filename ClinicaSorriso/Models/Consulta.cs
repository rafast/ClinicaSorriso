using System;
using System.Globalization;

namespace ClinicaSorriso.Models
{
    public class Consulta
    {
        public Paciente Paciente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan TempoDeConsulta { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }

        public Consulta(Paciente paciente, DateTime data, string horaInicio, string horaFim)
        {
            Paciente = paciente;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            TempoDeConsulta = GetTempoDeConsulta();
            paciente.MarcarConsulta(this);
            FormataHorario();
        }
        public bool TemChoqueDeHorario(Consulta consulta)
        {
            string horaInicio = RemovePonto(HoraInicio);
            string horaFim = RemovePonto(HoraFim);
            string consultaHoraInicio = RemovePonto(consulta.HoraInicio);
            string consultaHoraFim = RemovePonto(consulta.HoraFim);

            return !(double.Parse(horaInicio) > double.Parse(consultaHoraFim) || double.Parse(horaFim) < double.Parse(consultaHoraInicio));
        }
        private void FormataHorario()
        {
            if (!(HoraInicio.Contains(":") && HoraFim.Contains(":")))
            {
                HoraInicio = HoraInicio.Insert(2, ":");
                HoraFim = HoraFim.Insert(2, ":");
            }
        }

        private TimeSpan GetTempoDeConsulta()
        {
            string hrInicio = HoraInicio;
            string hrFim = HoraFim;

            if(HoraInicio.Contains(":") && HoraFim.Contains(":"))
            {
                hrInicio = HoraInicio.Replace(":", "");
                hrFim = HoraFim.Replace(":", "");

            }
            DateTime dtHoraInicio = RetornaDt(hrInicio);
            DateTime dtHoraFim = RetornaDt(hrFim);
            TimeSpan duration = dtHoraFim.Subtract(dtHoraInicio);
            return duration;
        
        }
        private DateTime RetornaDt(string str)
        {
            string horarioGeral = str;
            string hora = horarioGeral.Substring(0, 2);
            string minuto = horarioGeral.Substring(2, 2);
            DateTime data = Data.AddHours(double.Parse(hora)).AddMinutes(double.Parse(minuto));
            return data;
        }

        private string RemovePonto(string str)
        {
            return str.Replace(":", "");          
        }
        
    }
}
