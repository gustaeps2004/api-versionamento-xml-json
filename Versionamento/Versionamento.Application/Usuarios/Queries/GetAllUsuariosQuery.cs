using MediatR;

namespace Versionamento.Application.Usuarios.Queries
{
    public class GetAllUsuariosQuery : IRequest<IEnumerable<Domain.Entities.Usuarios>>
    {
    }
}
