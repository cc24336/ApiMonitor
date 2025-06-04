using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorApi
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }  // Chave primária explícita

        public int DiaSemana { get; set; }

        public string HorarioAtendimento { get; set; } = null!; // Valor não nulo com inicialização

        [ForeignKey("Monitor")]
        public int IdMonitor { get; set; }

        public Monitor Monitor { get; set; } = null!;
    }
}
