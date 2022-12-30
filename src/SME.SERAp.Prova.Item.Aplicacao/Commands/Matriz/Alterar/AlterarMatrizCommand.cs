using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarMatrizCommand : IRequest<bool>
    {
        public AlterarMatrizCommand(Matriz matriz)
        {
            Matriz = matriz;
        }

        public Matriz Matriz { get; set; }
    }
}