using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class TipoGradeDto
    {
        public TipoGradeDto(long id, long matrizId, string descricao, int ordem, StatusGeral status)
        {
            Id = id;
            MatrizId = matrizId;
            Descricao = descricao;
            Ordem = ordem;
            Status = status;
        }

        public long Id { get; }
        public long MatrizId { get; private set; }
        public string Descricao { get; }
        public int Ordem { get; }
        public StatusGeral Status { get; private set; }

        public bool Validacao()
        {
            return Id > 0 && MatrizId > 0 && !string.IsNullOrEmpty(Descricao);
        }

        public void AtribuirMatrizId(long matrizId)
        {
            MatrizId = matrizId;
        }

        public void AtribuirStatus(StatusGeral status)
        {
            Status = status;
        }
    }
}
