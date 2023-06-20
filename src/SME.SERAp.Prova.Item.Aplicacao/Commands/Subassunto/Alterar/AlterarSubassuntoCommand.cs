using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarSubassuntoCommand : IRequest<bool>
    {
        public AlterarSubassuntoCommand(Subassunto subassunto)
        {
            Subassunto = subassunto;
        }

        public Subassunto Subassunto { get; set; }
    }
}
