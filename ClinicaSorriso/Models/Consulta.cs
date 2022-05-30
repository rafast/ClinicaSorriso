using System;

namespace ClinicaSorriso.Models
{
    public class Consulta
    {
        public Paciente Paciente { get; set; }
        public DateTime Data { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }

        public Consulta(Paciente paciente, DateTime data, string horaInicio, string horaFim)
        {
            Paciente = paciente;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
        /*/public bool TemChoqueDeHorario(Consulta consulta) 
        {
            !(dataHoraInicio > intervalo.dataHoraFinal || dataHoraFinal < intervalo.dataHoraInicio);

        } adsdas lucas rafael */
    }
}
