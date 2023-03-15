using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosDificuldadesQueryHandler : IRequestHandler<ObterTodosDificuldadesQuery, IEnumerable<Dificuldade>>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public ObterTodosDificuldadesQueryHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public async Task<IEnumerable<Dificuldade>> Handle(ObterTodosDificuldadesQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDificuldade.ObterTudoAsync();
        }
    }
}
