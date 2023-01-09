using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarGrupoCommand : IRequest<long>
    {
        public AlterarGrupoCommand(Grupo grupo)
        {
            Grupo = grupo;
        }

        public Grupo Grupo { get; set; }
    }
}
