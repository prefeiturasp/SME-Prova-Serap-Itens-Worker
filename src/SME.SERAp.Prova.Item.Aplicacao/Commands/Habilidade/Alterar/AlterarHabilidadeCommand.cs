using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarHabilidadeCommand : IRequest<long>
    {
        public AlterarHabilidadeCommand(Habilidade habilidade)
        {
            Habilidade = habilidade;
        }

        public Habilidade Habilidade { get; set; }
    }
}
