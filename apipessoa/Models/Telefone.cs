namespace apipessoa.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Ddd { get; set; }
        public int Tipo { get; set; }
        public TelefoneTipo TipoObj { get; set; } = new TelefoneTipo();
    }
}
