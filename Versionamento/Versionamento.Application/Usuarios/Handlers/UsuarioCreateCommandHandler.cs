using FluentValidation;
using MediatR;
using Versionamento.Application.Usuarios.Commands;
using Versionamento.Application.Validation.Usuarios;
using Versionamento.Domain.Interfaces;

namespace Versionamento.Application.Usuarios.Handlers
{
    public class UsuarioCreateCommandHandler : IRequestHandler<UsuariosCreateCommand, Domain.Entities.Usuarios>
    {

        private readonly IMediator _mediator;
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioCommandHandlerValidationCreate _validation;

        public UsuarioCreateCommandHandler(IMediator mediator, IUsuarioRepository repository, UsuarioCommandHandlerValidationCreate validation)
        {
            _mediator = mediator;
            _repository = repository;
            _validation = validation;
        }

        public async Task<Domain.Entities.Usuarios> Handle(UsuariosCreateCommand request, CancellationToken cancellationToken)
        {
            await _validation.ValidateAsync(request);
            var usuario = new Domain.Entities.Usuarios(request.Nome, request.DocumentoFederal, request.DtNasc);
            if(usuario is null)
                throw new Exception("Erro ao criar a entidade!");

            _repository.CriarUsuario(usuario);
            return usuario;
        }
    }
}
