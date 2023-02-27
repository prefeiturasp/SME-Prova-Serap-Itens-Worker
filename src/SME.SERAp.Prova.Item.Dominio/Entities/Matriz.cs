using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Matriz : EntidadeBase
    {
        public Matriz(long? id, long legadoId, long disciplinaId, string descricao, string modelo, StatusGeral status)
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
            Modelo = modelo;
            DisciplinaId = disciplinaId;
            Status = (int)status;
        }

        public long LegadoId { get; }
        public string Descricao { get; }
        public string Modelo { get; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; }
        public long DisciplinaId { get; }
        public int Status { get; }

        public bool PossuiAlteracao(string descricao, string modelo, long disciplinaId, StatusGeral status)
        {
            return Descricao != descricao || Modelo != modelo || DisciplinaId != disciplinaId || Status != (int)status;
        }
    }
}
