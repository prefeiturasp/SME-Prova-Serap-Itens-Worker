using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public  class Matriz : EntidadeBase
    {
        public Matriz() { }
        public Matriz(string descricao)
        {
            Descricao = descricao;
        }
        public string Descricao { get; set; }
    }
}