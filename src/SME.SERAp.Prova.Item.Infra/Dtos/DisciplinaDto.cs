using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class DisciplinaDto
    {


        public DisciplinaDto() { }


        public DisciplinaDto(long id, string descricao, StatusGeral status)
        {
            Id = id;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; set; }
        public string Descricao { get; set; }
        public StatusGeral Status { get; set; }

        public bool Validacao()
        {
            return Id > 0 && !string.IsNullOrEmpty(Descricao);
        }

    }
}
