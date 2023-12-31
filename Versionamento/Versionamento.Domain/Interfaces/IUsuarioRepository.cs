﻿using Versionamento.Domain.Entities;

namespace Versionamento.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuarios>> GetAll();
        Task<Usuarios> GetByCodigo(Guid codigo);

        void CriarUsuario(Usuarios usuario);
        void AtualizarUsuario(string nome, string dtNasc, Guid codigo);
        void DeletarUsuario(Guid codigo);
    }
}
