using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirAreaConhecimentoCommand : IRequest<long>
    {
        public InserirAreaConhecimentoCommand(AreaConhecimento areaConhecimento)
        {
            AreaConhecimento = areaConhecimento;
        }

        public AreaConhecimento AreaConhecimento { get; set; }
    }
}