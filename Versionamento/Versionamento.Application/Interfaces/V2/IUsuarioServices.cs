﻿using Versionamento.Application.DTOs;

namespace Versionamento.Application.Interfaces.V2
{
    public interface IUsuarioServices
    {
        Task<object> GetAll(string contentType);
        Task<object> GetByCodigo(Guid codigo, string contentType);

        Task CriarUsuario(string usuariosDto, string contentType);
        Task AtualizarUsuario(string usuariosDto, Guid codigo, string contentType);
        Task DeletarUsuario(Guid codigo);
    }
}
