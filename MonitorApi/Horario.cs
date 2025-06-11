using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace MonitorApi
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }  

        public int DiaSemana { get; set; }

        public string HorarioAtendimento { get; set; } = null!; 

        [ForeignKey("Monitor")]
        public int IdMonitor { get; set; }

        [JsonIgnore]
        public Monitor Monitor { get; set; } = null!;
    }
}
