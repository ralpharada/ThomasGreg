namespace ThomasGreg.Application.Responses
{
    public class LogradouroResponse
    {
        public int Id { get; set; }
        public string NomeRua { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Cep { get; set; } = null!;
    }
}
