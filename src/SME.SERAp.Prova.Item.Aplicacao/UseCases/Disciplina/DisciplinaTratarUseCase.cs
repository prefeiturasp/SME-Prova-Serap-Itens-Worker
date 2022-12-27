using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
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


            if (disciplinaBase == null)
                return await Inserir(disciplinaMensagem);

            return await Alterar(disciplinaBase, disciplinaMensagem);
            // Buscar matrizes a partir da disciplina 
        }

        private async Task<bool> Inserir(DisciplinaDto disciplinaDto)
        {
            var disciplinaDominio = new Disciplina(null, disciplinaDto.Id, disciplinaDto.Descricao, StatusGeral.Ativo);
            await mediator.Send(new InserirDisciplinaCommand(disciplinaDominio));
            return true;
        }

        private async Task<bool> Alterar(Disciplina disciplinaBase, DisciplinaDto disciplinaDto)
        {
            var disciplinaDominio = new Disciplina(disciplinaBase.Id, disciplinaDto.Id, disciplinaDto.Descricao, disciplinaDto.Status);
            disciplinaDominio.CriadoEm = disciplinaBase.CriadoEm;
            await mediator.Send(new AlterarDisciplinaCommand(disciplinaDominio));
            return true;
        }
    }
}