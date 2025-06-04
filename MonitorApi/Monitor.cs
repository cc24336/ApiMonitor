using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MonitorApi
{
    public class Monitor
    {
        [Key]
        public int IdMonitor { get; set; }
        public string RA { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }

        public List<Horario> Horarios { get; set; } = new();
    }
}
