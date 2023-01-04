using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirHabilidadeCommand : IRequest<long>
    {
        public InserirHabilidadeCommand(Habilidade habilidade)
        {
            Habilidade = habilidade;
        }

        public Habilidade Habilidade { get; set; }
    }
}
