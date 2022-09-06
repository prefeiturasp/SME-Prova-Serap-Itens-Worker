using System;

namespace SME.SERAp.Prova.Item.Infra.Exceptions
{
    public class NegocioException : Exception
    {
        public NegocioException(string mensagem) : base(mensagem)
        {
        }
    }
}
