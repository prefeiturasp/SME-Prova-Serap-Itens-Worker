using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Subassunto : EntidadeBase
    {
        public Subassunto(long? id, long legadoId, long assuntoId, string descricao, StatusGeral status)
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
            AssuntoId = assuntoId;
            Descricao = descricao;
            Status = (int)status;
        }

        public long LegadoId { get; }
        public long AssuntoId { get; private set; }
        public string Descricao { get; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; }
        public int Status { get; }

        public void AtribuirAssuntoId(long assuntoId)
        {
            AssuntoId = assuntoId;
        }

        public bool PossuiAlteracao(long assuntoId, string descricao, StatusGeral status)
        {
            return AssuntoId != assuntoId || Descricao != descricao || Status != (int)status;
        }
    }
}