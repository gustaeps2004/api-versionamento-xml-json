using AutoMapper;
using Versionamento.Application.Interfaces.V2;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Services.V2
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public Task<object> GetAll(string contentType)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetByCodigo(Guid codigo, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task CriarUsuario(string usuariosDto, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarUsuario(string usuariosDto, Guid codigo, string contentType)
        {
            throw new NotImplementedException();
        }        

        public Task DeletarUsuario(Guid codigo)
        {
            throw new NotImplementedException();
        }        
    }
}
