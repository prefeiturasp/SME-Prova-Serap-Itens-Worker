using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarAssuntoCommand : IRequest<bool>
    {
        public AlterarAssuntoCommand(Assunto assunto)
        {
            Assunto = assunto;
        }

        public Assunto Assunto { get; set; }
    }
}
