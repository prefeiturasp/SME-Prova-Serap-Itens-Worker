using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirTipoGradeCommand : IRequest<long>
    {
        public InserirTipoGradeCommand(TipoGrade tipoGrade)
        {
            TipoGrade = tipoGrade;
        }

        public TipoGrade TipoGrade { get; set; }
    }
}
