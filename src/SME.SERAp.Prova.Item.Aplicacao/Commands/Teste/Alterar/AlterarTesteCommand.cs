using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarTesteCommand : IRequest<bool>
    {
        public AlterarTesteCommand(TesteDto testeDto)
        {
            TesteDto = testeDto;
        }

        public TesteDto TesteDto { get; set; }
    }
}
