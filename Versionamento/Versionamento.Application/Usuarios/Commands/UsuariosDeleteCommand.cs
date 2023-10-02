namespace Versionamento.Application.Usuarios.Commands
{
    public class UsuariosDeleteCommand : UsuariosCommand
    {
        public Guid Codigo { get; set; }

        public UsuariosDeleteCommand(Guid codigo)
        {
            Codigo = codigo;
        }
    }
}
