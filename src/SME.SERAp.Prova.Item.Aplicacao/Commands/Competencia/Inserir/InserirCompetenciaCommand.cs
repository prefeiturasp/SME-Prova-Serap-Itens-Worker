using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirCompetenciaCommand : IRequest<long>
    {
        public InserirCompetenciaCommand(Competencia competencia)
        {
            Competencia = competencia;
        }

        public Competencia Competencia { get; set; }
    }
}
