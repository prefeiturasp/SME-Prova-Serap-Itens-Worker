using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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