using Versionamento.Application.DTOs;

namespace Versionamento.Application.Interfaces
{
    public interface IUsuarioServices
    {
        Task<Object> GetAll(string accept);
        Task<Object> GetByCodigo(Guid codigo, string accept);

        Task CriarUsuario(UsuariosDto usuariosDto);
        Task AtualizarUsuario(UsuariosDto usuariosDto, Guid codigo);
        Task DeletarUsuario(Guid codigo);
    }
}
