using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirAssuntoCommand : IRequest<long>
    {
        public InserirAssuntoCommand(Assunto assunto)
        {
            Assunto = assunto;
        }

        public Assunto Assunto { get; set; }
    }
}
