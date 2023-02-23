using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirGrupoCommand : IRequest<long>
    {
        public InserirGrupoCommand(Grupo grupo)
        {
            Grupo = grupo;
        }

        public Grupo Grupo { get; set; }
    }
}
