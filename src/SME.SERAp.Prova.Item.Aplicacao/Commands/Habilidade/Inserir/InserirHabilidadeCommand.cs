using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirHabilidadeCommand : IRequest<long>
    {
        public InserirHabilidadeCommand(Dominio.Entities.Habilidade habilidade)
        {
            Habilidade = habilidade;
        }

        public Dominio.Entities.Habilidade Habilidade { get; set; }
    }
}
