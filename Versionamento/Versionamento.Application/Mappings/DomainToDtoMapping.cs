using AutoMapper;
using Versionamento.Application.DTOs;
using Versionamento.Domain.Entities;

namespace Versionamento.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Domain.Entities.Usuarios, UsuariosDto>().ReverseMap(); 
        }
    }
}
