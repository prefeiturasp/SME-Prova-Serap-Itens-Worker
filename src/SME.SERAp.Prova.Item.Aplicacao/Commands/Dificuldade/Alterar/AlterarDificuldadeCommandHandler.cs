using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarDificuldadeCommandHandler : IRequestHandler<AlterarDificuldadeCommand, long>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public AlterarDificuldadeCommandHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public async Task<long> Handle(AlterarDificuldadeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioDificuldade.UpdateAsync(request.Dificuldade);
        }
    }
}
