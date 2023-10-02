using AutoMapper;
using Versionamento.Application.DTOs;
using Versionamento.Application.Usuarios.Commands;

namespace Versionamento.Application.Mappings
{
    public class DtoToCommandMappingProfile : Profile
    {
        public DtoToCommandMappingProfile()
        {
            CreateMap<UsuariosDto, UsuariosCreateCommand>();
        }
    }
}
