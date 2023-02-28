using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Habilidade : EntidadeBase
    {
        public Habilidade()
        {
        }

        public Habilidade(long legadoId, long competenciaId, string codigo, string descricao)
        {
            LegadoId = legadoId;
            CompetenciaId = competenciaId;
            Codigo = codigo;
            Descricao = descricao;
            CriadoEm = AlteradoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public long LegadoId { get; set; }
        public long CompetenciaId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public StatusGeral Status { get; set; }

        public void Alterar(long competenciaId, string codigo, string descricao)
        {
            CompetenciaId = competenciaId;
            Codigo = codigo;
            Descricao = descricao;
            AlteradoEm = DateTime.Now;
        }

        public void Inativar()
        {
            Status = StatusGeral.Inativo;
        }

        public bool PossuiAlteracao(long competenciaId, string codigo, string descricao, StatusGeral status)
        {
            return CompetenciaId != competenciaId || Codigo != codigo || Descricao != descricao || Status != status;
        }
    }
}
