using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class GetSimplesApiSerapQuery : IRequest<string>
    {
        public GetSimplesApiSerapQuery(string requestUri)
        {
            RequestUri = requestUri;
        }

        public string RequestUri { get; set; }

    }
}
