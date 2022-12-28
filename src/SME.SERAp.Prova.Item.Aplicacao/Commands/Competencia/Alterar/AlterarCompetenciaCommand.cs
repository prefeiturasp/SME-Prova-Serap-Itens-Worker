using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarCompetenciaCommand : IRequest<bool>
    {
        public AlterarCompetenciaCommand(Competencia competencia)
        {
            Competencia = competencia;
        }

        public Competencia Competencia { get; set; }
    }
}
