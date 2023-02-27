using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Disciplina : EntidadeBase
    {
        public Disciplina(long? id, long legadoId, long areaConhecimentoId, string descricao, string nivelEnsino, StatusGeral status)
        {
            if (id == null)
            {
                CriadoEm = AlteradoEm = DateTime.Now;
                Status = (int)StatusGeral.Ativo;
            }
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
            }

            LegadoId = legadoId;
            Descricao = descricao;
            NivelEnsino = nivelEnsino;
            Status = (int)status;
            AreaConhecimentoId = areaConhecimentoId;
        }

        public long LegadoId { get; }
        public string Descricao { get; }
        public string NivelEnsino { get; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; }
        public long AreaConhecimentoId { get; }
        public int Status { get; }

        public bool PossuiAlteracao(string descricao, string nivelEnsino, long areaConhecimentoId, StatusGeral status)
        {
            return Descricao != descricao || NivelEnsino != nivelEnsino || AreaConhecimentoId != areaConhecimentoId ||
                   Status != (int)status;
        }
    }
}