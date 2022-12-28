using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarDisciplinaCommandHandler : IRequestHandler<AlterarDisciplinaCommand, long>
    {

        private readonly IRepositorioDisciplina repositorioDisciplina;

        public AlterarDisciplinaCommandHandler(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina ?? throw new ArgumentNullException(nameof(repositorioDisciplina));
        }

        public async Task<long> Handle(AlterarDisciplinaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioDisciplina.UpdateAsync(request.Disciplina);
        }
    }
}