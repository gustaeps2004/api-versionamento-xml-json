using AutoMapper;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Linq;
using System.Xml;
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

        public async Task<Object> GetAll(string accept)
        {
            var usuarios = _mapper.Map<IEnumerable<UsuariosDto>>(await _usuarioRepository.GetAll());

            if(accept != "application/xml")
            {
                return usuarios;
            }

            XmlDocument usuariosXml = new XmlDocument();
            
            using (var reader = JsonReaderWriterFactory
                .CreateJsonReader(Encoding.UTF8
                .GetBytes(JsonConvert.SerializeObject(usuarios)), XmlDictionaryReaderQuotas.Max))
            {
                XElement xml = XElement.Load(reader);
                usuariosXml.LoadXml(xml.ToString());

                
                
                return usuariosXml.InnerXml;
            }
        }

        public async Task<Object> GetByCodigo(Guid codigo, string accept)
        {
            var usuario = _mapper.Map<UsuariosDto>(await _usuarioRepository.GetByCodigo(codigo));

            if (accept != "application/xml")
            {
                return usuario;
            }

            var usuarioXml = JsonConvert.DeserializeXNode(JsonConvert.SerializeObject(usuario), "usuarios");
            return usuarioXml.Document;
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
