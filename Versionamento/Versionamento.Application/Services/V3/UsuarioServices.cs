using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Versionamento.Application.DTOs;
using Versionamento.Application.Interfaces.V3;
using Versionamento.Application.Usuarios.Commands;
using Versionamento.Application.Usuarios.Queries;
using Versionamento.Application.Validation.Usuarios;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Services.V3
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<object> GetAll(string contentType)
        {
            var getAllUsuariosQuery = new GetAllUsuariosQuery() ?? throw new Exception("Entidade não pode ser carregada!");

            var usuarios = await _mediator.Send(getAllUsuariosQuery);
            if (contentType != "application/xml")
            {
                return usuarios;
            }

            XmlDocument usuariosXml = new();

            using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8
                .GetBytes(JsonConvert.SerializeObject(usuarios)), XmlDictionaryReaderQuotas.Max))
            {
                XElement xml = XElement.Load(reader);
                usuariosXml.LoadXml(xml.ToString());

                return usuariosXml.InnerXml;
            }
        }

        public async Task<object> GetByCodigo(Guid codigo, string contentType)
        {
            var usuarioByCodigoQuery = new GetUsuarioByCodigoQuery(codigo) ?? throw new Exception("Entidade não pode ser carregada!");

            var usuario = await _mediator.Send(usuarioByCodigoQuery);
            if (contentType != "application/xml")
            {
                return usuario;
            }

            
            var usuarioXml = JsonConvert.DeserializeXNode(JsonConvert.SerializeObject(usuario), "usuarios");
            return usuarioXml.Document;
        }

        public async Task CriarUsuario(string usuariosDto, string contentType)
        {
            if (contentType != "application/xml")
            {
                var newUsuariosDto = JsonConvert.DeserializeObject<UsuariosDto>(usuariosDto.ToString());
                await _mediator.Send(_mapper.Map<UsuariosCreateCommand>(newUsuariosDto));
            }
            else
            {
                XmlSerializer serializer = new(typeof(UsuariosDto));
                using (TextReader reader = new StringReader(usuariosDto))
                {
                    UsuariosDto usuarioXmlToJson = (UsuariosDto)serializer.Deserialize(reader);
                    await _mediator.Send(_mapper.Map<UsuariosCreateCommand>(usuarioXmlToJson));
                }
            }
        }

        public async Task AtualizarUsuario(string usuariosDto, Guid codigo, string contentType)
        {
            if (await GetByCodigo(codigo, "application/json") is null)
                throw new Exception("Usuário não encontrado");

            if (contentType != "application/xml")
            {                
                var newUsuariosDto = JsonConvert.DeserializeObject<UsuariosDto>(usuariosDto.ToString());
                newUsuariosDto.Codigo = codigo;

                await _mediator.Send(_mapper.Map<UsuariosUpdateCommand>(newUsuariosDto));                
            }
            else
            {
                XmlSerializer serializer = new(typeof(UsuariosDto));
                using (TextReader reader = new StringReader(usuariosDto))
                {
                    UsuariosDto usuarioXmlToJson = (UsuariosDto)serializer.Deserialize(reader);
                    usuarioXmlToJson.Codigo = codigo;

                    await _mediator.Send(_mapper.Map<UsuariosUpdateCommand>(usuarioXmlToJson));
                }
            }
        }

        public async Task DeletarUsuario(Guid codigo)
        {
            var usuarioDeletarCommand = new UsuariosDeleteCommand(codigo) ?? throw new Exception("Entidade não pode ser carregada!");
            await _mediator.Send(usuarioDeletarCommand);
        }
    }
}
