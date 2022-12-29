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

                if (matrizAtual == null)
                    return await Inserir(matriz);

                return await Alterar(matrizAtual, matriz);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
          
        }

        private async Task<bool> Inserir(MatrizDto matrizDto)
        {
            var MatrizInserir = new Matriz(null, matrizDto.Id, matrizDto.DisciplinaId, matrizDto.Descricao, StatusGeral.Ativo);
            await mediator.Send(new InserirMatrizCommand(MatrizInserir));
            return true;
        }

        private async Task<bool> Alterar(Matriz matriz, MatrizDto matrizApi)
        {
            var matrizAlterar = new Matriz(matriz.Id, matrizApi.Id, matrizApi.DisciplinaId, matrizApi.Descricao, matrizApi.Status);
            matrizAlterar.CriadoEm = matriz.CriadoEm;
            await mediator.Send(new AlterarMatrizCommand(matrizAlterar));
            return true;
        }
    }
}