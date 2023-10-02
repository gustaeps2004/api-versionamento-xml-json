using MediatR;
using Versionamento.Application.Usuarios.Queries;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Usuarios.Handlers
{
    public class GetAllUsuariosQueryHandler : IRequestHandler<GetAllUsuariosQuery, IEnumerable<Domain.Entities.Usuarios>>
    {
        private readonly IUsuarioRepository _repository;

        public GetAllUsuariosQueryHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Usuarios>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }
    }
}
