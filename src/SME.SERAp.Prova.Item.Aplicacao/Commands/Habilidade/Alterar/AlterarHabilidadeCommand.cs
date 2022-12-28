using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarHabilidadeCommand : IRequest<long>
    {
        public AlterarHabilidadeCommand(Dominio.Entities.Habilidade habilidade)
        {
            Habilidade = habilidade;
        }

        public Dominio.Entities.Habilidade Habilidade { get; set; }
    }
}
