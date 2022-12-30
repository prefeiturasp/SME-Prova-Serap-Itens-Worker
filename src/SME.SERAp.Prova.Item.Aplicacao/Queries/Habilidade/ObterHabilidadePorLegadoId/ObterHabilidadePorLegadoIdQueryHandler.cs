using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadePorLegadoIdQueryHandler : IRequestHandler<ObterHabilidadePorLegadoIdQuery, Habilidade>
    {
        private readonly IRepositorioHabilidade repositorioHabilidade;

        public ObterHabilidadePorLegadoIdQueryHandler(IRepositorioHabilidade repositorioHabilidade)
        {
            this.repositorioHabilidade = repositorioHabilidade ?? throw new ArgumentNullException(nameof(repositorioHabilidade));
        }

        public async Task<Habilidade> Handle(ObterHabilidadePorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioHabilidade.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
