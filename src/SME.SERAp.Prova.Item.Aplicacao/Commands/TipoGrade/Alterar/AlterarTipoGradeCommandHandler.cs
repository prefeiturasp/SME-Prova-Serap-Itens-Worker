using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarTipoGradeCommandHandler : IRequestHandler<AlterarTipoGradeCommand, bool>
    {
        private readonly IRepositorioTipoGrade repositorioTipoGrade;

        public AlterarTipoGradeCommandHandler(IRepositorioTipoGrade repositorioTipoGrade)
        {
            this.repositorioTipoGrade = repositorioTipoGrade ?? throw new ArgumentNullException(nameof(repositorioTipoGrade));
        }

        public async Task<bool> Handle(AlterarTipoGradeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTipoGrade.UpdateAsync(request.TipoGrade) > 0;
        }
    }
}
