using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodasDisciplinasQueryHandler : IRequestHandler<ObterTodasDisciplinasQuery, IEnumerable<Disciplina>>
    {

        private readonly IRepositorioDisciplina repositorioDisciplina;

        public ObterTodasDisciplinasQueryHandler(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina ?? throw new ArgumentNullException(nameof(repositorioDisciplina));
        }

        public async Task<IEnumerable<Disciplina>> Handle(ObterTodasDisciplinasQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDisciplina.ObterTudoAsync();
        }
    }
}