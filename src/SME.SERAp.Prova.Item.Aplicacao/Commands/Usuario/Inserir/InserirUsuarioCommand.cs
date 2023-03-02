using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirUsuarioCommand : IRequest<long>
    {
        public InserirUsuarioCommand(Usuario usuario)
        {
            Usuario = usuario;
        }

        public Usuario Usuario { get; set; }
    }
}
