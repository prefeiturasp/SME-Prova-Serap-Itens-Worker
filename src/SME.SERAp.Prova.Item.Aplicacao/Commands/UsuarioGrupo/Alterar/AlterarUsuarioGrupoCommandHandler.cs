using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.UsuarioGrupo.Alterar
{
    public class AlterarUsuarioGrupoCommandHandler : IRequestHandler<AlterarUsuarioGrupoCommand, long>
    {
        private readonly IRepositorioUsuarioGrupo repositorioUsuarioGrupo;

        public AlterarUsuarioGrupoCommandHandler(IRepositorioUsuarioGrupo repositorioUsuarioGrupo)
        {
            this.repositorioUsuarioGrupo = repositorioUsuarioGrupo ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupo));
        }

        public Task<long> Handle(AlterarUsuarioGrupoCommand request, CancellationToken cancellationToken)
        {
            return repositorioUsuarioGrupo.UpdateAsync(request.UsuarioGrupo);
        }
    }
}
