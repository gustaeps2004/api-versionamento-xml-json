using AutoMapper;
using Newtonsoft.Json;
using System.Xml.Linq;
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

        public async Task<IEnumerable<UsuariosDto>> GetAll(string typeFormat)
        {
            var usuarios = _mapper.Map<IEnumerable<UsuariosDto>>(await _usuarioRepository.GetAll());

            if(typeFormat != "application/xml")
            {
                return usuarios;
            }
            return usuarios;
            //XNode node = JsonConvert.DeserializeXNode(usuarios, "root");

        }

        public async Task<UsuariosDto> GetByCodigo(Guid codigo, string typeFormat)
        {
            var usuario = _mapper.Map<UsuariosDto>(await _usuarioRepository.GetByCodigo(codigo));

            if (typeFormat != "application/xml")
            {
                return usuario;
            }

            XNode node = JsonConvert.DeserializeXNode(JsonConvert.SerializeObject(usuario), "usuario");

            return usuario;
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
