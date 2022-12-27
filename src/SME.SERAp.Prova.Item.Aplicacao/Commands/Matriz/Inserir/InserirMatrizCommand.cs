using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirMatrizCommand : IRequest<long>
    {
        public InserirMatrizCommand(Matriz matriz)
        {
            Matriz = matriz;
        }

        public Matriz Matriz { get; set; }
    }
}