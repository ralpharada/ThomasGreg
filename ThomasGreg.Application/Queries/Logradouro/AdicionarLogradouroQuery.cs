using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class AdicionarLogradouroQuery : Request<IEvent>
    {
        public int ClienteId { get; private set; }
        public string? NomeRua { get; private set; }
        public string? Numero { get; private set; } 
        public string? Bairro { get; private set; } 
        public string? Cidade { get; private set; } 
        public string? Estado { get; private set; } 
        public string? Cep { get; private set; }
                     
        public AdicionarLogradouroQuery(int clienteId,
            string? nomeRua,
            string? numero,
            string? bairro,
            string? cidade,
            string? estado,
            string? cep)
        {
            ClienteId = clienteId;
            NomeRua = nomeRua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }
    }
}
