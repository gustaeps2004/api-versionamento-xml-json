using Versionamento.Domain.Entities;

namespace Versionamento.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuarios>> GetAll();
        Task<Usuarios> GetByCodigo(Guid codigo);

        Task CriarUsuario(Usuarios usuario);
        Task AtualizarUsuario(Usuarios usuario, Guid codigo);
        Task DeletarUsuario(Guid codigo);
    }
}
