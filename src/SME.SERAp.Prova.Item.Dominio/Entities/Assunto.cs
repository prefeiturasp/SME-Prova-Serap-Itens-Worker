using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Assunto : EntidadeBase
    {
        public Assunto()
        {
        }

        public Assunto(long? id, long legadoId, long disciplinaId, string descricao, StatusGeral status)
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
            DisciplinaId = disciplinaId;
            Descricao = descricao;
            Status = (int)status;
        }

        public long LegadoId { get; set; }
        public long DisciplinaId { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }

        public bool PossuiAlteracao(string descricao, StatusGeral status)
        {
            return Descricao != descricao || Status != (int)status;
        }
    }
}