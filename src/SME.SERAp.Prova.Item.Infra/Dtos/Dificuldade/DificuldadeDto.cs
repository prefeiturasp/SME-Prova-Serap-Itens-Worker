using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Dificuldade
{
    public class DificuldadeDto
    {
        public DificuldadeDto()
        {

        }

        public DificuldadeDto(long id, string descricao, int ordem, StatusGeral status)
        {
            Id = id;
            Descricao = descricao;
            Status = status;
            Ordem = ordem;
        }

        public long Id { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public StatusGeral Status { get; set; }

        public bool Validacao()
        {
            return Id > 0 && !string.IsNullOrEmpty(Descricao);
        }

    }
}
