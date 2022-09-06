using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Teste.Inserir
{
    internal class AlterarTesteCommandHandler : IRequestHandler<AlterarTesteCommand, bool>
    {
        private readonly IRepositorioTeste repositorioTeste;

        public AlterarTesteCommandHandler(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste ?? throw new ArgumentNullException(nameof(repositorioTeste));
        }

        public async Task<bool> Handle(AlterarTesteCommand request, CancellationToken cancellationToken)
        {
            var teste = new Dominio.Entities.Teste(request.TesteDto.Descricao)
            {
                Id = request.TesteDto.Id
            };

            return await repositorioTeste.UpdateAsync(teste) > 0;
        }
    }
}
