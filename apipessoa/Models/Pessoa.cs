namespace apipessoa.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public long Cpf { get; set; }
        public int Endereco { get; set; }
        public Endereco EnderecoObj { get; set; } = new Endereco();
        public List<Telefone> Telefones { get; set; } 
    }
}
