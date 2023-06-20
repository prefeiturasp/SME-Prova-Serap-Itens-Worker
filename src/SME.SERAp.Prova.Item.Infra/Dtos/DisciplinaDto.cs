using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class DisciplinaDto
    {
        public DisciplinaDto(long id, long areaConhecimentoId, string descricao, string nivelEnsino, StatusGeral status)
        {
            Id = id;
            Descricao = descricao;
            NivelEnsino = nivelEnsino;
            Status = status;
            AreaConhecimentoId = areaConhecimentoId;
        }

        public long Id { get; }
        public string Descricao { get; }
        public string NivelEnsino { get; }
        public StatusGeral Status { get; }
        public long AreaConhecimentoId { get; private set; }

        public bool Validacao()
        {
            return Id > 0 && !string.IsNullOrEmpty(Descricao) && !string.IsNullOrEmpty(NivelEnsino);
        }

        public void AtribuirAreaConhecimentoId(long areaConhecimentoId)
        {
            AreaConhecimentoId = areaConhecimentoId;
        }
    }
}
