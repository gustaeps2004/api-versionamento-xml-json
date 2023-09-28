using AutoMapper;
using Versionamento.Application.DTOs;
using Versionamento.Application.Interfaces;
using Versionamento.Domain.Entities;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Services
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

        public async Task<IEnumerable<UsuariosDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<UsuariosDto>>(await _usuarioRepository.GetAll());            
        }

        public async Task<UsuariosDto> GetByCodigo(Guid codigo)
        {
            return _mapper.Map<UsuariosDto>(await _usuarioRepository.GetByCodigo(codigo));
        }

        public async Task CriarUsuario(UsuariosDto usuariosDto)
        {
            await _usuarioRepository.CriarUsuario(_mapper.Map<Usuarios>(usuariosDto));
        }

        public async Task AtualizarUsuario(UsuariosDto usuariosDto, Guid codigo)
        {
            await _usuarioRepository.AtualizarUsuario(_mapper.Map<Usuarios>(usuariosDto), codigo);
        }        

        public async Task DeletarUsuario(Guid codigo)
        {
            await _usuarioRepository.DeletarUsuario(codigo);
        }        
    }
}
