using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirGrupoCommandHandler : IRequestHandler<InserirGrupoCommand, long>
    {
        private readonly IRepositorioGrupo repositorioGrupo;

        public InserirGrupoCommandHandler(IRepositorioGrupo repositorioGrupo)
        {
            this.repositorioGrupo = repositorioGrupo ?? throw new ArgumentNullException(nameof(repositorioGrupo));
        }

        public async Task<long> Handle(InserirGrupoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioGrupo.IncluirAsync(request.Grupo);
        }
    }
}
