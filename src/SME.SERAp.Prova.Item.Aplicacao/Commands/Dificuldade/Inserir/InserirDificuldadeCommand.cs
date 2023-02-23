using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirDificuldadeCommand : IRequest<long>
    {
        public InserirDificuldadeCommand(Dificuldade dificuldade)
        {
            Dificuldade = dificuldade;
        }

        public Dificuldade Dificuldade { get; set; }
    }
}
