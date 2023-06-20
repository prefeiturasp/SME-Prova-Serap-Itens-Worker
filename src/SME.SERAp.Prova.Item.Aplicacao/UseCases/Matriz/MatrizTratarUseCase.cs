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
            var matriz = mensagemRabbit.ObterObjetoMensagem<MatrizDto>();

            if (matriz == null)
                return false;

            if (!matriz.Validacao())
                return false;

            var matrizBase = await mediator.Send(new ObterMatrizPorLegadoIdQuery(matriz.Id));

            bool retornoInserirAlterarMatriz;

            if (matrizBase == null)
                retornoInserirAlterarMatriz = await Inserir(matriz);
            else
                retornoInserirAlterarMatriz = await Alterar(matrizBase, matriz);

            if (!retornoInserirAlterarMatriz)
                return false;

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.CompetenciaSync, matriz.Id));
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TipoGradeSync, matriz.Id));

            return true;
        }

        private async Task<bool> Inserir(MatrizDto matriz)
        {
            var matrizInserir = new Matriz(null, matriz.Id, matriz.DisciplinaId, matriz.Descricao,
                matriz.Modelo, StatusGeral.Ativo);

            await mediator.Send(new InserirMatrizCommand(matrizInserir));

            return true;
        }

        private async Task<bool> Alterar(Matriz matrizBase, MatrizDto matriz)
        {
            if (!matrizBase.PossuiAlteracao(matriz.Descricao, matriz.Modelo, matriz.DisciplinaId, matriz.Status))
                return true;

            var matrizAlterar = new Matriz(matrizBase.Id, matriz.Id, matriz.DisciplinaId, matriz.Descricao,
                matriz.Modelo, matriz.Status)
            {
                CriadoEm = matrizBase.CriadoEm
            };

            await mediator.Send(new AlterarMatrizCommand(matrizAlterar));

            return true;
        }
    }
}