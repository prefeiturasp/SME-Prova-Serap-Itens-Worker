using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class TipoGrade : EntidadeBase
    {
        public TipoGrade()
        {
        }

        public TipoGrade(long? id, long legadoId, long matrizId, string descricao, int ordem, StatusGeral status)
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
            MatrizId = matrizId;
            Descricao = descricao;
            Ordem = ordem;
            Status = (int)status;
        }

        public long LegadoId { get; set; }
        public long MatrizId { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }

        public bool PossuiAlteracao(long matrizId, string descricao, int ordem, StatusGeral status)
        {
            return MatrizId != matrizId || Descricao != descricao || Ordem != ordem || Status != (int)status;
        }
    }
}
