using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinasPorLegadoIdQueryHandler : IRequestHandler<ObterDisciplinasPorLegadoIdQuery, Disciplina>
    {

        private readonly IRepositorioDisciplina repositorioDisciplina;

        public ObterDisciplinasPorLegadoIdQueryHandler(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina ?? throw new ArgumentNullException(nameof(repositorioDisciplina));
        }

        public async Task<Disciplina> Handle(ObterDisciplinasPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDisciplina.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}