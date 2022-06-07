using System;

namespace ClinicaSorriso.Models
{
    // Classe que representa uma consulta na aplicação
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
        }

        //Recebe uma consulta e retorna se há um conflito de horário com a mesma
        public bool TemConflitoDeHorario(Consulta consulta)
        {
            int.TryParse(HoraInicio, out int horaInicio);
            int.TryParse(HoraFim, out int horaFim);
            int.TryParse(consulta.HoraInicio, out int consultaHoraInicio);
            int.TryParse(consulta.HoraFim, out int consultaHoraFim);

            return !(horaInicio >= consultaHoraFim || horaFim <= consultaHoraInicio);
        }        

        //Retorna a duração da consulta
        private TimeSpan GetTempoDeConsulta()
        {
            var horaInicio = new TimeSpan(int.Parse(HoraInicio.Substring(0,2)), int.Parse(HoraInicio.Substring(2)), 0);
            var horaFim = new TimeSpan(int.Parse(HoraFim.Substring(0, 2)), int.Parse(HoraFim.Substring(2)), 0);
            return horaFim - horaInicio;
        
        }

        //Retorna o horário no formato HH:MM
        public string GetHorario(string horarioStr)
        {
            return horarioStr.Insert(2, ":");
        }
        
    }
}
