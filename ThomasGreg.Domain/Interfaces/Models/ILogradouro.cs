namespace ThomasGreg.Domain.Interfaces
{
    public interface ILogradouro
    {
      int Id { get; set; }
      string NomeRua { get; set; }
      string Numero { get; set; }
      string Bairro { get; set; }
      string Cidade { get; set; }
      string Estado { get; set; }
      string Cep { get; set; }
    }
}