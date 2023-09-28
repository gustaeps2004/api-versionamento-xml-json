using AutoMapper;
using Versionamento.Application.DTOs;
using Versionamento.Application.Interfaces;

namespace Versionamento.Application.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioRepository(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<UsuariosDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UsuariosDto> GetByCodigo(Guid codigo)
        {
            throw new NotImplementedException();
        }

        public Task CriarUsuario(UsuariosDto usuariosDto)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarUsuario(UsuariosDto usuariosDto, Guid codigo)
        {
            throw new NotImplementedException();
        }        

        public Task DeletarUsuario(Guid codigo)
        {
            throw new NotImplementedException();
        }        
    }
}
