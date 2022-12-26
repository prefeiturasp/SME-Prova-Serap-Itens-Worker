using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Api.AreaConhecimento;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ExecutarSincronizacaoAreaConhecimento : AbstractUseCase, IExecutarSincronizacaoAreaConhecimento
    {
        private readonly IServicoLog serviceLog;
        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ExecutarSincronizacaoAreaConhecimento(IMediator mediator, IServicoLog serviceLog) : base(mediator)
        {
            this.serviceLog = serviceLog ?? throw new ArgumentNullException(nameof(serviceLog));
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            // Buscar Areas conhecimento API 
            var listaAreaConhecimentosApi =  await mediator.Send(new ObterAreaConhecimentoQuery());
            if (listaAreaConhecimentosApi == null && !listaAreaConhecimentosApi.Any())
                return false;

            var  listaIdsApi = listaAreaConhecimentosApi.Select(a => a.Id).ToList();
            // Busca Areas conhecimento Banco
            var listaAreaConhecimentoBase =  await repositorioAreaConhecimento.ObterTudoAsync();
            if(listaAreaConhecimentoBase != null && listaAreaConhecimentoBase.Any())
            {
                var listaIdsBase =  listaAreaConhecimentoBase.Select(ac => ac.Id).ToList();

                var areasNovas = listaAreaConhecimentosApi.Where(a => !listaIdsBase.Contains(a.Id)).ToList();

                if (areasNovas != null && areasNovas.Any())
                {
                   var areasConhecimentos = areasNovas.Select(ac => new AreaConhecimento()
                    {
                        AreaConhecimentoLegadoId = ac.Id,
                        Descricao = ac.Descricao,
                    }).ToList();

                    await mediator.Send(new InserirDresCommand(dresNovasParaIncluir));
                }

            }
         
            var todosIdsAreaConhecimento = listaAreaConhecimentosApi.Select(a => a.Id).ToList();
            foreach (var areaConheciomentoApi in listaAreaConhecimentosApi.ToList())
            {

            }
            listaAreaConhecimentosApi.ToList().ForEach(x => x.Id = listaAreaConhecimentoBase.Where(y => y.Id));
            foreach

            // Inclui novas Areas conhecimento 

            // Chama fila disciplinas 

            return true;

        }
    }
}
