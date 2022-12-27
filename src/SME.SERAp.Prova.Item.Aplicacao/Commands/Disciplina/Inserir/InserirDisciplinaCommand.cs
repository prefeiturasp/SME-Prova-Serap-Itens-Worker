using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirDisciplinaCommand : IRequest<long>
    {
        public InserirDisciplinaCommand(Disciplina disciplina)
        {
            Disciplina = disciplina;
        }

        public Disciplina Disciplina { get; set; }
    }
}