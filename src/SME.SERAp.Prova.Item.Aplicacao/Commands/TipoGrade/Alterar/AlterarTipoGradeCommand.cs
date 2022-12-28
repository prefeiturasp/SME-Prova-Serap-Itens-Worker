using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarTipoGradeCommand : IRequest<bool>
    {
        public AlterarTipoGradeCommand(TipoGrade tipoGrade)
        {
            TipoGrade = tipoGrade;
        }

        public TipoGrade TipoGrade { get; set; }
    }
}
