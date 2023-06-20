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
            var disciplina = mensagemRabbit.ObterObjetoMensagem<DisciplinaDto>();

            if (disciplina == null)
                return false;

            if (!disciplina.Validacao())
                return false;

            var disciplinaBase = await mediator.Send(new ObterDisciplinaPorLegadoIdQuery(disciplina.Id));

            long disciplinaBaseId;

            if (disciplinaBase != null)
            {
                await Alterar(disciplinaBase, disciplina);
                disciplinaBaseId = disciplinaBase.Id;
            }
            else
                disciplinaBaseId = await Inserir(disciplina);

            if (disciplinaBaseId != 0)
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.MatrizSync,
                    disciplina.Id.ToString()));
            }

            return true;
        }

        private async Task<long> Inserir(DisciplinaDto disciplina)
        {
            var disciplinaDominio = new Disciplina(null, disciplina.Id, disciplina.AreaConhecimentoId,
                disciplina.Descricao, disciplina.NivelEnsino, StatusGeral.Ativo);

            return await mediator.Send(new InserirDisciplinaCommand(disciplinaDominio));
        }

        private async Task Alterar(Disciplina disciplinaBase, DisciplinaDto disciplina)
        {
            if (!disciplinaBase.PossuiAlteracao(disciplina.Descricao, disciplina.NivelEnsino,
                    disciplina.AreaConhecimentoId, disciplina.Status))
            {
                return;
            }

            var disciplinaDominio = new Disciplina(disciplinaBase.Id, disciplina.Id, disciplina.AreaConhecimentoId,
                disciplina.Descricao, disciplina.NivelEnsino, disciplina.Status)
            {
                CriadoEm = DateTime.Now.Date
            };

            await mediator.Send(new AlterarDisciplinaCommand(disciplinaDominio));
        }
    }
}