using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirSubassuntoCommand : IRequest<long>
    {
        public InserirSubassuntoCommand(Subassunto subassunto)
        {
            Subassunto = subassunto;
        }

        public Subassunto Subassunto { get; set; }
    }
}
