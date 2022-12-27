using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaPorLegadoIdQueryHandler : IRequestHandler<ObterAreaPorLegadoIdQuery, AreaConhecimento>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterAreaPorLegadoIdQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<AreaConhecimento> Handle(ObterAreaPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}
