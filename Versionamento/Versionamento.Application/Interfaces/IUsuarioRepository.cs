using Versionamento.Application.DTOs;

namespace Versionamento.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuariosDto>> GetAll();
        Task<UsuariosDto> GetByCodigo(Guid codigo);

        Task CriarUsuario(UsuariosDto usuariosDto);
        Task AtualizarUsuario(UsuariosDto usuariosDto, Guid codigo);
        Task DeletarUsuario(Guid codigo);
    }
}
