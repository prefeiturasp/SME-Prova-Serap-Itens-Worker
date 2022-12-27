﻿using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Matriz : EntidadeBase
    {

        public Matriz()
        {

        }

        public Matriz(long? id, long legadoId, string descricao, StatusGeral status)
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

        public long LegadoId { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public long DisciplinaId { get; set; }
        public int Status { get; set; }

    }
}
