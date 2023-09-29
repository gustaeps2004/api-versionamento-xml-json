using Microsoft.EntityFrameworkCore;
using Versionamento.Domain.Entities;
using Versionamento.Domain.Interfaces;
using Versionamento.Infra.Data.Context;

namespace Versionamento.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuarios>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();  
        }

        public async Task<Usuarios> GetByCodigo(Guid codigo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
        }        

        public void CriarUsuario(Usuarios usuario)
        {
            string sql = "INSERT INTO [Usuarios] (Codigo, Nome, DocumentoFederal, DtNasc)"
                + $" VALUES ('{Guid.NewGuid()}', '{usuario.Nome}', "
                + $"'{usuario.DocumentoFederal}', '{usuario.DtNasc.ToString("yyyy-MM-dd")}');";

            _context.Database.ExecuteSqlRaw(sql);
        }

        public void AtualizarUsuario(Usuarios usuario, Guid codigo)
        {
            string sql = "UPDATE Usuarios SET"
                        + $" Nome = '{usuario.Nome}', DtNasc = '{usuario.DtNasc.ToString("yyyy-MM-dd")}'"
                        + $" WHERE Codigo = '{codigo}'";

            _context.Database.ExecuteSqlRaw(sql);
        }

        public void DeletarUsuario(Guid codigo)
        {
            string sql = "DELETE FROM Usuarios"
                     + $" WHERE Codigo = '{codigo}';";

            _context.Database.ExecuteSqlRaw(sql);
        }
    }
}
