using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class MatrizTratarUseCase : AbstractUseCase, IMatrizTratarUseCase
    {
        public MatrizTratarUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            try
            {
                var matriz = mensagemRabbit.ObterObjetoMensagem<MatrizDto>();

                if (matriz == null) return false;
                if (!matriz.Validacao()) return false;

                var matrizAtual = await mediator.Send(new ObterMatrizPorLegadoIdQuery(matriz.Id));

                var retornoInserirAlterarMatriz = true;
                if (matrizAtual == null)
                    retornoInserirAlterarMatriz = await Inserir(matriz);
                else
                    retornoInserirAlterarMatriz = await Alterar(matrizAtual, matriz);

                if (retornoInserirAlterarMatriz)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.CompetenciaSync, matriz.Id));
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TipoGradeSync, matriz.Id));
                }

                return retornoInserirAlterarMatriz;

            }
            catch (System.Exception)
            {
                throw;
            }

        }

        private async Task<bool> Inserir(MatrizDto matrizDto)
        {
            var MatrizInserir = new Matriz(null, matrizDto.Id, matrizDto.DisciplinaId, matrizDto.Descricao, matrizDto.Modelo, StatusGeral.Ativo);
            await mediator.Send(new InserirMatrizCommand(MatrizInserir));
            return true;
        }

        private async Task<bool> Alterar(Matriz matriz, MatrizDto matrizApi)
        {
            var matrizAlterar = new Matriz(matriz.Id, matrizApi.Id, matrizApi.DisciplinaId, matrizApi.Descricao, matrizApi.Modelo, matrizApi.Status);
            matrizAlterar.CriadoEm = matriz.CriadoEm;
            await mediator.Send(new AlterarMatrizCommand(matrizAlterar));
            return true;
        }
    }
}