using MediatR;
using Versionamento.Application.Usuarios.Commands;
using Versionamento.Application.Validation.Usuarios;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Usuarios.Handlers
{
    public class UsuarioUpdateCommandHandler : IRequestHandler<UsuariosUpdateCommand, Domain.Entities.Usuarios>
    {
        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioCommandHandlerValidationUpdate _validation;

        public UsuarioUpdateCommandHandler(IMediator mediator, IUsuarioRepository repository, UsuarioCommandHandlerValidationUpdate validation)
        {
            _mediator = mediator;
            _repository = repository;
            _validation = validation;
        }

        public async Task<Domain.Entities.Usuarios> Handle(UsuariosUpdateCommand request, CancellationToken cancellationToken)
        {
            await _validation.ValidateAsync(request);
            var usuario = new Domain.Entities.Usuarios(request.Codigo, request.Nome, request.DocumentoFederal, request.DtNasc);
            if (usuario is null)
                throw new Exception("Erro ao criar a entidade!");

            _repository.AtualizarUsuario(usuario.Nome, usuario.DtNasc.ToString("yyyy-MM-dd"), usuario.Codigo);
            return usuario;
        }
    }
}
