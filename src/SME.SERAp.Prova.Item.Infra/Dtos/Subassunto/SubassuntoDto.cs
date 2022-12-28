using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class SubassuntoDto
    {
        public SubassuntoDto()
        {

        }

        public SubassuntoDto(long id, long assuntoId, string descricao, StatusGeral status)
        {
            Id = id;
            AssuntoId = assuntoId;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; set; }
        public long AssuntoId { get; set; }
        public string Descricao { get; set; }
        public StatusGeral Status { get; set; }

        public bool Validacao()
        {
            return Id > 0 && AssuntoId > 0 && !string.IsNullOrEmpty(Descricao);
        }
    }
}
