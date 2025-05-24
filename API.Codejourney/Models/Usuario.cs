using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Codejourney.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string? Senha { get; set; }

        public DateTime? DataCadastro { get; set; }

    }
}
    