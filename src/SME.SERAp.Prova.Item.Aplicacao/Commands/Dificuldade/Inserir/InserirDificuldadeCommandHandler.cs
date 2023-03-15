using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirDificuldadeCommandHandler : IRequestHandler<InserirDificuldadeCommand, long>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public InserirDificuldadeCommandHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public Task<long> Handle(InserirDificuldadeCommand request, CancellationToken cancellationToken)
        {
            return repositorioDificuldade.IncluirAsync(request.Dificuldade);
        }
    }
}
