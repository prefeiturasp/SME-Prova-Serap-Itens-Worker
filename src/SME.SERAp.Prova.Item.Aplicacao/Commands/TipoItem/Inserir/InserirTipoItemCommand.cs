using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirTipoItemCommand : IRequest<long>
    {
        public InserirTipoItemCommand(TipoItem tipoItem)
        {
            TipoItem = tipoItem;
        }

        public TipoItem TipoItem { get; set; }
    }
}
