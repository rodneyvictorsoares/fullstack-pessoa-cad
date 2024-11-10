using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apppessoa.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public long Cpf { get; set; }
        public Endereco EnderecoObj { get; set; } = new Endereco();
        public List<Telefone> Telefones { get; set; } = new List<Telefone>();
    }
}
