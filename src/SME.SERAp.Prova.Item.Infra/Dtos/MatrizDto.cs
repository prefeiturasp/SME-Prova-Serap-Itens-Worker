using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class MatrizDto
    {

        public MatrizDto(long id, long disciplinaId, string descricao, StatusGeral status)
        {
            Id = id;
            DisciplinaId = disciplinaId;
            Descricao = descricao;
            Status = status;
        }

        public long Id { get; }
        public long DisciplinaId { get; private set; }
        public string Descricao { get; }
        public string Modelo { get; set; }
        public StatusGeral Status { get; }

        public bool Validacao()
        {
            return Id > 0 && DisciplinaId > 0 && !string.IsNullOrEmpty(Descricao) && !string.IsNullOrEmpty(Modelo);
        }

        public void AtribuirDisciplinaId(long disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }
    }
}
