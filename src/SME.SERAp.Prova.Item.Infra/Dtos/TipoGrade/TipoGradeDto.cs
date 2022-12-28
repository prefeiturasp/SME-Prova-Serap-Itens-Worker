using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class TipoGradeDto
    {
        public TipoGradeDto()
        {

        }

        public TipoGradeDto(long id, long matrizId, string descricao, int ordem, StatusGeral status)
        {
            Id = id;
            MatrizId = matrizId;
            Descricao = descricao;
            Ordem = ordem;
            Status = status;
        }

        public long Id { get; set; }
        public long MatrizId { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public StatusGeral Status { get; set; }

        public bool Validacao()
        {
            return Id > 0 && MatrizId > 0 && !string.IsNullOrEmpty(Descricao);
        }

        public TipoGradeDto AlterarMatrizIdStatus(long matrizId, StatusGeral novoStatus)
        {
            MatrizId = matrizId;
            Status = novoStatus;
            return this;
        }

    }
}
