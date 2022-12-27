using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarAreaConhecimentoCommand : IRequest<long>
    {
        public AlterarAreaConhecimentoCommand(AreaConhecimento areaConhecimento)
        {
            AreaConhecimento = areaConhecimento;
        }

        public AreaConhecimento AreaConhecimento { get; set; }
    }
}