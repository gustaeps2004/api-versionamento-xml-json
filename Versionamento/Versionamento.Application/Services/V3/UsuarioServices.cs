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
using Versionamento.Application.Usuarios.Queries;
using Versionamento.Application.Validation.Usuarios;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Services.V3
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly CommandUsuariosDtoValidationCreate _validationCreate;
        private readonly CommandUsuariosDtoValidationUpdate _validationUpdate;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IMapper mapper, IMediator mediator,
            CommandUsuariosDtoValidationCreate validationCreate, CommandUsuariosDtoValidationUpdate validationUpdate)
        {
            _mapper = mapper;
            _mediator = mediator;
            _usuarioRepository = usuarioRepository;
            _validationCreate = validationCreate;
            _validationUpdate = validationUpdate;
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
                await _validationCreate.ValidateAsync(newUsuariosDto);

                _usuarioRepository.CriarUsuario(_mapper.Map<Domain.Entities.Usuarios>(newUsuariosDto));
            }
            else
            {
                XmlSerializer serializer = new(typeof(UsuariosDto));
                using (TextReader reader = new StringReader(usuariosDto))
                {
                    UsuariosDto usuarioXmlToJson = (UsuariosDto)serializer.Deserialize(reader);
                    await _validationCreate.ValidateAsync(usuarioXmlToJson);

                    _usuarioRepository.CriarUsuario(_mapper.Map<Domain.Entities.Usuarios>(usuarioXmlToJson));
                }
            }
        }

        public async Task AtualizarUsuario(string usuariosDto, Guid codigo, string contentType)
        {
            if (await _usuarioRepository.GetByCodigo(codigo) is null)
                throw new Exception("Usuário não encontrado");

            if (contentType != "application/xml")
            {
                var newUsuariosDto = JsonConvert.DeserializeObject<UsuariosDto>(usuariosDto.ToString());
                await _validationUpdate.ValidateAsync(newUsuariosDto);

                _usuarioRepository.AtualizarUsuario(
                    newUsuariosDto.Nome,
                    newUsuariosDto.DtNasc.ToString("yyyy-MM-dd"),
                    codigo
                );
            }
            else
            {
                XmlSerializer serializer = new(typeof(UsuariosDto));
                using (TextReader reader = new StringReader(usuariosDto))
                {
                    UsuariosDto usuarioXmlToJson = (UsuariosDto)serializer.Deserialize(reader);
                    await _validationUpdate.ValidateAsync(usuarioXmlToJson);

                    _usuarioRepository.AtualizarUsuario(
                       usuarioXmlToJson.Nome,
                       usuarioXmlToJson.DtNasc.ToString("yyyy-MM-dd"),
                       codigo
                    );
                }
            }
        }

        public async Task DeletarUsuario(Guid codigo)
        {
            if (await _usuarioRepository.GetByCodigo(codigo) is null)
                throw new Exception("Usuário não encontrado");

            _usuarioRepository.DeletarUsuario(codigo);
        }
    }
}
