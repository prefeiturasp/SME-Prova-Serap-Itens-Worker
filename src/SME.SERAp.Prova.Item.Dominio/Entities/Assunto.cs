using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Assunto : EntidadeBase
    {
        public Assunto(long? id, long legadoId, string descricao, StatusGeral status)
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
            Status = (int)status;
        }

        public long LegadoId { get; }
        public string Descricao { get; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; }
        public int Status { get; }

        public bool PossuiAlteracao(string descricao, StatusGeral status)
        {
            return Descricao != descricao || Status != (int)status;
        }
    }
}