using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarDisciplinaCommand : IRequest<long>
    {
        public AlterarDisciplinaCommand(Disciplina disciplina)
        {
            Disciplina = disciplina;
        }

        public Disciplina Disciplina { get; set; }
    }
}