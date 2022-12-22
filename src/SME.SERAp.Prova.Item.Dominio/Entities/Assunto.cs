using SME.SERAp.Prova.Item.Dominio.Entities;
using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Assunto : EntidadeBase
    {

        public Assunto()
        {

        }

        public Assunto(long? id, long legadoId, string descricao, int status)
        {
            if (id == null)
            {
                CriadoEm = AlteradoEm = DateTime.Now;
                Status = 1;
            }
            else
            {
                Id = Id;
                AlteradoEm = DateTime.Now;
            }

            LegadoId = legadoId;
            Descricao = descricao;
            Status = status;
        }

        public long LegadoId { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }

    }
}