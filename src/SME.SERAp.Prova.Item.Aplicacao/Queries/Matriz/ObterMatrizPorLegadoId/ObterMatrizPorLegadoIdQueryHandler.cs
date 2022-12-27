﻿using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizPorLegadoIdQueryHandler : IRequestHandler<ObterMatrizPorLegadoIdQuery, Matriz>
    {

        private readonly IRepositorioMatriz repositorioMatriz;

        public ObterMatrizPorLegadoIdQueryHandler(IRepositorioMatriz repositorioMatriz)
        {
            this.repositorioMatriz = repositorioMatriz ?? throw new ArgumentNullException(nameof(repositorioMatriz));
        }

        public async Task<Matriz> Handle(ObterMatrizPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioMatriz.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}