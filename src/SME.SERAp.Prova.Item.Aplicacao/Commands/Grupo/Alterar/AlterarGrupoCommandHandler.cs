using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Grupo.Alterar
{
    public class AlterarGrupoCommandHandler : IRequestHandler<AlterarGrupoCommand, long>
    {
        private readonly IRepositorioGrupo repositorioGrupo;

        public AlterarGrupoCommandHandler(IRepositorioGrupo repositorioGrupo)
        {
            this.repositorioGrupo = repositorioGrupo ?? throw new ArgumentNullException(nameof(repositorioGrupo));
        }

        public async Task<long> Handle(AlterarGrupoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioGrupo.UpdateAsync(request.Grupo);
        }
    }
}
