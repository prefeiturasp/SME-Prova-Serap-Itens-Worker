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
    public class ObterTodasAreasConhecimentosQueryHandler : IRequestHandler<ObterTodasAreasConhecimentosQuery, IEnumerable<AreaConhecimento>>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterTodasAreasConhecimentosQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<IEnumerable<AreaConhecimento>> Handle(ObterTodasAreasConhecimentosQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterTudoAsync();
        }
    }
}