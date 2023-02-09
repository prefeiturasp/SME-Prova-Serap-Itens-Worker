using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class TipoItem : EntidadeBase
    {
        public TipoItem()
        {

        }

        public TipoItem(long? id, long legadoId, bool ehPadrao, int qtdeAlternativa, string descricao, int status)
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
            EhPadrao = ehPadrao;
            QtdeAlternativa = qtdeAlternativa;
            Descricao = descricao;
            Status = status;
        }

        public long LegadoId { get; set; }
        public bool EhPadrao { get; set; }
        public int QtdeAlternativa { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }

        public bool PossuiAlteracao(bool ehPadrao, int qtdeAlternativa, string descricao, int status)
        {
            return EhPadrao != ehPadrao || QtdeAlternativa != qtdeAlternativa || Descricao != descricao || Status != status;
        }

    }
}