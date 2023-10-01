﻿using AutoMapper;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using Versionamento.Application.DTOs;
using Versionamento.Domain.Entities;
using Versionamento.Domain.Interfaces;
using System.Xml.Serialization;
using Versionamento.Application.Interfaces.V1;
using Versionamento.Application.Validation.Usuarios;

namespace Versionamento.Application.Services.V1
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly CommandUsuariosDtoValidationCreate _validationCreate;
        private readonly CommandUsuariosDtoValidationUpdate _validationUpdate;

        public UsuarioServices(IUsuarioRepository usuarioRepository, IMapper mapper, CommandUsuariosDtoValidationCreate validation, CommandUsuariosDtoValidationUpdate validationUpdate)
        {
            _usuarioRepository = usuarioRepository;
            _validationCreate = validation;
            _validationUpdate = validationUpdate;
            _mapper = mapper;
        }

        public async Task<object> GetAll(string contentType)
        {
            var usuarios = _mapper.Map<IEnumerable<UsuariosDto>>(await _usuarioRepository.GetAll());

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
            var usuario = _mapper.Map<UsuariosDto>(await _usuarioRepository.GetByCodigo(codigo)) ??
                throw new Exception("Usuário não encontrado");

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

                _usuarioRepository.CriarUsuario(_mapper.Map<Usuarios>(newUsuariosDto));
            }
            else
            {
                XmlSerializer serializer = new(typeof(UsuariosDto));
                using (TextReader reader = new StringReader(usuariosDto))
                {
                    UsuariosDto usuarioXmlToJson = (UsuariosDto)serializer.Deserialize(reader);
                    await _validationCreate.ValidateAsync(usuarioXmlToJson);

                    _usuarioRepository.CriarUsuario(_mapper.Map<Usuarios>(usuarioXmlToJson));
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