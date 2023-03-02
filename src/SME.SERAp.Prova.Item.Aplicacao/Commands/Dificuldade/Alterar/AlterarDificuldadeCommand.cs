using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarDificuldadeCommand : IRequest<long>
    {
        public AlterarDificuldadeCommand(Dificuldade dificuldade)
        {
            Dificuldade = dificuldade;
        }

        public Dificuldade Dificuldade { get; set; }
    }
}
