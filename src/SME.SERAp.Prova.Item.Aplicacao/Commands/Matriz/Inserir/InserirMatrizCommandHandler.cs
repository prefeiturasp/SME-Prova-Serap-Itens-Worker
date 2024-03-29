﻿using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirMatrizCommandHandler : IRequestHandler<InserirMatrizCommand, long>
    {

        private readonly IRepositorioMatriz repositorioMatriz;

        public InserirMatrizCommandHandler(IRepositorioMatriz repositorioMatriz)
        {
            this.repositorioMatriz = repositorioMatriz ?? throw new ArgumentNullException(nameof(repositorioMatriz));
        }

        public async Task<long> Handle(InserirMatrizCommand request, CancellationToken cancellationToken)
        {
            return await repositorioMatriz.IncluirAsync(request.Matriz);
        }
    }
}
