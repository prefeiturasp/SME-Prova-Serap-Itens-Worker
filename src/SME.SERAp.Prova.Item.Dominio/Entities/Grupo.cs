using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Grupo : EntidadeBase
    {
        public Grupo() { }

        public Grupo(Guid legadoId, string nome, bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
        {
            LegadoId = legadoId;
            Nome = nome;
            PermiteConsultar = permiteConsultar;
            PermiteInserir = permiteInserir;
            PermiteAlterar = permiteAlterar;
            PermiteExcluir = permiteExcluir;
            CriadoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Alterar(string nome, bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
        {
            Nome = nome;
            PermiteConsultar = permiteConsultar;
            PermiteInserir = permiteInserir;
            PermiteAlterar = permiteAlterar;
            PermiteExcluir = permiteExcluir;
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Inativar()
        {
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Inativo;
        }

        public Guid LegadoId { get; set; }
        public string Nome { get; set; }
        public bool PermiteConsultar { get; set; }
        public bool PermiteInserir { get; set; }
        public bool PermiteAlterar { get; set; }
        public bool PermiteExcluir { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
        public StatusGeral Status { get; set; }
    }
}
