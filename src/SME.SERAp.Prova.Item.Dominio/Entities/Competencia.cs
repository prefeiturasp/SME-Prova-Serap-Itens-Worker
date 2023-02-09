using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Competencia : EntidadeBase
    {
        public Competencia()
        {

        }

        public Competencia(long? id, string codigo, long legadoId, long matrizId, string descricao, int status)
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

            Codigo = codigo;
            LegadoId = legadoId;
            MatrizId = matrizId;
            Descricao = descricao;
            Status = status;
        }

        public string Codigo { get; set; }
        public long LegadoId { get; set; }
        public long MatrizId { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }

        public bool PossuiAlteracao(string codigo, long matrizId, string descricao, StatusGeral status)
        {
            return Codigo != codigo || MatrizId != matrizId || Descricao != descricao || Status != (int)status;
        }

        public Competencia AlterarStatus(StatusGeral status)
        {
            Status = (int)status;
            return this;
        }

    }
}
