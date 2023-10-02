using MediatR;
using Versionamento.Application.Usuarios.Queries;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Usuarios.Handlers
{
    public class GetUsuarioByCodigoQueryHandler : IRequestHandler<GetUsuarioByCodigoQuery, Domain.Entities.Usuarios>
    {
        private readonly IUsuarioRepository _repository;

        public GetUsuarioByCodigoQueryHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Usuarios> Handle(GetUsuarioByCodigoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByCodigo(request.Codigo);
        }
    }
}
