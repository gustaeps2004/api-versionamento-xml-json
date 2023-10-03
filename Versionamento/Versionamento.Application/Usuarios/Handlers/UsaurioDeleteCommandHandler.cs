using MediatR;
using Versionamento.Application.Usuarios.Commands;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Usuarios.Handlers
{
    public class UsaurioDeleteCommandHandler : IRequestHandler<UsuariosDeleteCommand, Domain.Entities.Usuarios>
    {
        private readonly IUsuarioRepository _repository;

        public UsaurioDeleteCommandHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Usuarios> Handle(UsuariosDeleteCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _repository.GetByCodigo(request.Codigo);
            if (usuario is null)
                throw new Exception("Código de usuário incorreto!");

            _repository.DeletarUsuario(request.Codigo);
            return usuario;
        }
    }
}
