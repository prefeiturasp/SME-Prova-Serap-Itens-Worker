using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosGruposQueryHandler : IRequestHandler<ObterTodosGruposQuery, IEnumerable<Grupo>>
    {
        private readonly IRepositorioGrupo repositorioGrupo;

        public ObterTodosGruposQueryHandler(IRepositorioGrupo repositorioGrupo)
        {
            this.repositorioGrupo = repositorioGrupo ?? throw new ArgumentNullException(nameof(repositorioGrupo));
        }

        public async Task<IEnumerable<Grupo>> Handle(ObterTodosGruposQuery request, CancellationToken cancellationToken)
        {
            return await repositorioGrupo.ObterTudoAsync();
        }
    }
}
