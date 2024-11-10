
using System.ComponentModel.DataAnnotations;

namespace webpessoa.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Você precisa informar um número de telefone.")]
        public int Numero { get; set; }
        [Required(ErrorMessage ="O numero DDD é Obrigatório!")]
        [Display(Name ="DDD")]
        public int Ddd { get; set; }
        [Required(ErrorMessage ="Por Favor escolha um tipo de telefone.")]
        public int Tipo { get; set; }
        public TelefoneTipo TipoObj { get; set; } = new TelefoneTipo();
    }
}
