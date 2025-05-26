using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Codejourney.Models
{
    public class Jornadas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string? JornNome { get; set; }

        public string? JornFase { get; set; }

        public int JornEstrelas { get; set; }

        public int JornUltimaFase { get; set; }

    }
}
