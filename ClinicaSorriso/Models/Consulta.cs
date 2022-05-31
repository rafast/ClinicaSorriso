using System;
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
            Paciente.MarcarConsulta(this);
            TempoDeConsulta = GetTempoDeConsulta();
        }
        public bool TemChoqueDeHorario(Consulta consulta)
        {
            string horaInicio = RemovePonto(HoraInicio);
            string horaFim = RemovePonto(HoraFim);
            string consultaHoraInicio = RemovePonto(consulta.HoraInicio);
            string consultaHoraFim = RemovePonto(consulta.HoraFim);

            return !(double.Parse(horaInicio) > double.Parse(consultaHoraFim) || double.Parse(horaFim) < double.Parse(consultaHoraInicio));
        }

        public TimeSpan GetTempoDeConsulta()
        {
            DateTime d1 = RetornaDt(HoraInicio);
            DateTime d2 = RetornaDt(HoraFim);
            TimeSpan duration = d2.Subtract(d1);
            return duration;
        
        }

        private DateTime RetornaDt(string s)
        {
            string horarioGeral = RemovePonto(s);
            string hora = horarioGeral.Substring(0, 2);
            string minuto = horarioGeral.Substring(2, 2);
            DateTime data = Data.AddHours(double.Parse(hora)).AddMinutes(double.Parse(minuto));
            return data;
        }

        private string  RemovePonto(string s)
        {
            return s.Replace(":", "");
        }
    }
}
