using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class CompetenciaDto
    {
        public CompetenciaDto(long id, string codigo, long matrizId, string descricao, StatusGeral status)
        {
            Id = id;
            Codigo = codigo;
            MatrizId = matrizId;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; }
        public string Codigo { get; }
        public long MatrizId { get; private set; }
        public string Descricao { get; }
        public StatusGeral Status { get; private set; }

        public bool Validacao()
        {
            return Id > 0 && !string.IsNullOrEmpty(Codigo) && MatrizId > 0 && !string.IsNullOrEmpty(Descricao);
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
