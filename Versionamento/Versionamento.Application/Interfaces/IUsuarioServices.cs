using Versionamento.Application.DTOs;

namespace Versionamento.Application.Interfaces
{
    public interface IUsuarioServices
    {
        Task<IEnumerable<UsuariosDto>> GetAll(string typeFormat);
        Task<UsuariosDto> GetByCodigo(Guid codigo, string typeFormat);

        Task CriarUsuario(UsuariosDto usuariosDto);
        Task AtualizarUsuario(UsuariosDto usuariosDto, Guid codigo);
        Task DeletarUsuario(Guid codigo);
    }
}
