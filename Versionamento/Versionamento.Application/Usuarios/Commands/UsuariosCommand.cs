using MediatR;

namespace Versionamento.Application.Usuarios.Commands
{
    public class UsuariosCommand : IRequest<Domain.Entities.Usuarios>
    {
        public string Nome { get; set; }
        public string DocumentoFederal { get; set; }
        public DateTime DtNasc { get; set; }
    }
}
