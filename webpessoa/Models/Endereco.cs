using System.ComponentModel.DataAnnotations;

namespace webpessoa.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="O logradouro é informação obrigatória!")]
        [Display(Name ="Logradouro")]
        public string Logradouro { get; set; } = string.Empty;

        public int? Numero { get; set; }
        public int? Cep { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
