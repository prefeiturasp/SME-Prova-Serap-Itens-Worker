using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.UsuarioGrupo.Inserir
{
    public class InserirUsuarioGrupoCommandHandler : IRequestHandler<InserirUsuarioGrupoCommand, long>
    {
        private readonly IRepositorioUsuarioGrupo repositorioUsuarioGrupo;

        public InserirUsuarioGrupoCommandHandler(IRepositorioUsuarioGrupo repositorioUsuarioGrupo)
        {
            this.repositorioUsuarioGrupo = repositorioUsuarioGrupo ?? throw new ArgumentNullException(nameof(repositorioUsuarioGrupo));
        }

        public Task<long> Handle(InserirUsuarioGrupoCommand request, CancellationToken cancellationToken)
        {
            return repositorioUsuarioGrupo.IncluirAsync(request.UsuarioGrupo);
        }
    }
}
