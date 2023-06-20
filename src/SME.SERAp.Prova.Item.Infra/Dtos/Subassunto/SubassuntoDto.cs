using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class SubassuntoDto
    {
        public SubassuntoDto(long id, long assuntoId, string descricao, StatusGeral status)
        {
            Id = id;
            AssuntoId = assuntoId;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; }
        public long AssuntoId { get; private set; }
        public string Descricao { get; }
        public StatusGeral Status { get; }

        public bool Validacao()
        {
            return Id > 0 && AssuntoId > 0 && !string.IsNullOrEmpty(Descricao);
        }

        public void AtribuirAssuntoId(long assuntoId)
        {
            AssuntoId = assuntoId;
        }
    }
}
