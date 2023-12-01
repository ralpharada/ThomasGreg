using AutoMapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Mapper
{
    public class UsuarioMapper<T> where T : class
    {
        public static T Map(object source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, AdicionarUsuarioQuery>().ReverseMap();
                cfg.CreateMap<Usuario, AtualizarUsuarioQuery>().ReverseMap();
                cfg.CreateMap<Usuario, UsuarioResponse>().ReverseMap();
                cfg.CreateMap<Usuario, UsuarioLogadoResponse>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<T>(source);
        }
    }
}
