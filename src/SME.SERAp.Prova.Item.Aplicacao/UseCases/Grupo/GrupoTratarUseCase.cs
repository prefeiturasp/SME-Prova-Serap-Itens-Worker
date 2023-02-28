using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class GrupoTratarUseCase : AbstractUseCase, IGrupoTratarUseCase
    {
        public GrupoTratarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupoCoresso = mensagemRabbit.ObterObjetoMensagem<GrupoCoressoDto>();
            if (grupoCoresso == null) throw new Exception("grupo coresso inválido");

            var grupo = await mediator.Send(new ObterGrupoPorLegadoIdQuery(grupoCoresso.Id));
            
            if (grupo == null)
            {
                var inserir = new Grupo(
                    grupoCoresso.Id,
                    grupoCoresso.Nome,
                    grupoCoresso.PermiteConsultar,
                    grupoCoresso.PermiteInserir,
                    grupoCoresso.PermiteAlterar,
                    grupoCoresso.PermiteExcluir);

                await mediator.Send(new InserirGrupoCommand(inserir));
            }
            else
            {
                if (grupo.PossuiAlteracao(grupoCoresso.Nome, grupoCoresso.PermiteConsultar, grupoCoresso.PermiteInserir,
                    grupoCoresso.PermiteAlterar, grupoCoresso.PermiteExcluir))
                {
                    grupo.Alterar(grupoCoresso.Nome,
                        grupoCoresso.PermiteConsultar,
                        grupoCoresso.PermiteInserir,
                        grupoCoresso.PermiteAlterar,
                        grupoCoresso.PermiteExcluir);

                    await mediator.Send(new AlterarGrupoCommand(grupo));
                }
            }

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UsuarioSync, grupoCoresso.Id));

            return true;
        }
    }
}