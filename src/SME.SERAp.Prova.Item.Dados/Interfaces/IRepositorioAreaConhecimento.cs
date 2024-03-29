﻿using SME.SERAp.Prova.Item.Dominio;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioAreaConhecimento : IRepositorioBase<AreaConhecimento>
    {
        public Task<AreaConhecimento> ObterPorLegadoIdAsync(long legadoId);
    }
}
