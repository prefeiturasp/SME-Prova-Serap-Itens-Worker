using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class DisciplinaTratarUseCase : AbstractUseCase, IDisciplinaTratarUseCase
    {
        public DisciplinaTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var disciplinaMensagem = mensagemRabbit.ObterObjetoMensagem<DisciplinaDto>();

            if (disciplinaMensagem == null) return false;
            if (!disciplinaMensagem.Validacao()) return false;

            var disciplinaBase = await mediator.Send(new ObterDisciplinasPorLegadoIdQuery(disciplinaMensagem.Id));
            long disciplinaBaseId;
            if (disciplinaBase != null)
            {
                await Alterar(disciplinaBase.Id, disciplinaMensagem);
                disciplinaBaseId = disciplinaBase.Id;
            }
            else
                disciplinaBaseId = await Inserir(disciplinaMensagem);

            if (disciplinaBaseId != 0)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.MatrizSync, disciplinaMensagem.Id.ToString()));

            return true;
        }

        private async Task<long> Inserir(DisciplinaDto disciplinaDto)
        {
            var disciplinaDominio = new Disciplina(null, disciplinaDto.Id,disciplinaDto.AreaConhecimentoId, disciplinaDto.Descricao, disciplinaDto.NivelEnsino, StatusGeral.Ativo);
            return await mediator.Send(new InserirDisciplinaCommand(disciplinaDominio));
        }

        private async Task<bool> Alterar(long disciplinaBaseId, DisciplinaDto disciplinaDto)
        {
            var disciplinaDominio = new Disciplina(disciplinaBaseId, disciplinaDto.Id, disciplinaDto.AreaConhecimentoId, disciplinaDto.Descricao, disciplinaDto.NivelEnsino, disciplinaDto.Status);
            disciplinaDominio.CriadoEm = DateTime.Now.Date;
            await mediator.Send(new AlterarDisciplinaCommand(disciplinaDominio));
            return true;
        }
    }
}