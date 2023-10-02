using MediatR;

namespace Versionamento.Application.Usuarios.Queries
{
    public class GetUsuarioByCodigoQuery : IRequest<Domain.Entities.Usuarios>
    {
        public Guid Codigo { get; set; }

        public GetUsuarioByCodigoQuery(Guid codigo)
        {
            Codigo = codigo;
        }
    }
}
