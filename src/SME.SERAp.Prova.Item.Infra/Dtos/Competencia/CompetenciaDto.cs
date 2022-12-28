using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra
{
    public class CompetenciaDto
    {
        public CompetenciaDto()
        {

        }

        public CompetenciaDto(long id, string codigo, long matrizId, string descricao, StatusGeral status)
        {
            Id = id;
            Codigo = codigo;
            MatrizId = matrizId;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; set; }
        public string Codigo { get; set; }        
        public long MatrizId { get; set; }
        public string Descricao { get; set; }        
        public StatusGeral Status { get; set; }

        public bool Validacao()
        {
            return Id > 0 && !string.IsNullOrEmpty(Codigo) && MatrizId > 0 && !string.IsNullOrEmpty(Descricao);
        }

        public CompetenciaDto AlterarMatrizIdStatus(long matrizId, StatusGeral novoStatus)
        {
            MatrizId = matrizId;
            Status = novoStatus;
            return this;
        }

    }
}
