﻿using SME.SERAp.Prova.Item.Dominio.Entities;
using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Assunto : EntidadeBase
    {

        public Assunto()
        {

        }

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

        public long LegadoId { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }

    }
}