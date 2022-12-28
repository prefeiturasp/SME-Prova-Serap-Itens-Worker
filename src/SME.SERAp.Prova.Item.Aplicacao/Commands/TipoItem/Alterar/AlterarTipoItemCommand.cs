using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarTipoItemCommand : IRequest<bool>
    {
        public AlterarTipoItemCommand(TipoItem tipoItem)
        {
            TipoItem = tipoItem;
        }

        public TipoItem TipoItem { get; set; }
    }
}
