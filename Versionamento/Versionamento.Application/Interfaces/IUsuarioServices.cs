using Versionamento.Application.DTOs;

namespace Versionamento.Application.Interfaces
{
    public interface IUsuarioServices
    {
        Task<Object> GetAll(string accept);
        Task<Object> GetByCodigo(Guid codigo, string accept);

        Task CriarUsuario(object usuariosDto, string accept);
        Task AtualizarUsuario(object usuariosDto, Guid codigo, string accept);
        Task DeletarUsuario(Guid codigo, string accept);
    }
}
