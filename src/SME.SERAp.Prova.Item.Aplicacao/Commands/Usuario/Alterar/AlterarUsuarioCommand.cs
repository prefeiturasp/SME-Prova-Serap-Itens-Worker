using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarUsuarioCommand : IRequest<long>
    {
        public AlterarUsuarioCommand(Usuario usuario)
        {
            Usuario = usuario;
        }

        public Usuario Usuario { get; set; }
    }
}
