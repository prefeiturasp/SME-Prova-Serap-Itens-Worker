using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class TipoGrade : EntidadeBase
    {
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

        public long LegadoId { get; }
        public long MatrizId { get; }
        public string Descricao { get; }
        public int Ordem { get; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; }
        public int Status { get; }

        public bool PossuiAlteracao(long matrizId, string descricao, int ordem, StatusGeral status)
        {
            return MatrizId != matrizId || Descricao != descricao || Ordem != ordem || Status != (int)status;
        }
    }
}
