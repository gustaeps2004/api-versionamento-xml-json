using Versionamento.Application.DTOs;

namespace Versionamento.Application.Interfaces
{
    public interface IUsuarioServices
    {
        Task<Object> GetAll(string contentType);
        Task<Object> GetByCodigo(Guid codigo, string contentType);

        Task CriarUsuario(string usuariosDto, string contentType);
        Task AtualizarUsuario(object usuariosDto, Guid codigo, string contentType);
        Task DeletarUsuario(Guid codigo, string contentType);
    }
}
