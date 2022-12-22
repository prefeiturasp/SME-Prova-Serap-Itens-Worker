using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class TipoItemDto
    {
        public TipoItemDto()
        {

        }

        public TipoItemDto(long id, bool ehPadrao, int qtdeAlternativa, string descricao, StatusGeral status)
        {
            Id = id;
            EhPadrao = ehPadrao;
            QtdeAlternativa = qtdeAlternativa;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; set; }
        public bool EhPadrao { get; set; }
        public int QtdeAlternativa { get; set; }
        public string Descricao { get; set; }
        public StatusGeral Status { get; set; }

        public bool Validacao()
        {
            return Id > 0 && !string.IsNullOrEmpty(Descricao);
        }

    }
}
