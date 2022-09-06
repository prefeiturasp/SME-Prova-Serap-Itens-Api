using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Teste.Inserir
{
    internal class InserirTesteCommandHandler : IRequestHandler<InserirTesteCommand, long>
    {
        private readonly IRepositorioTeste repositorioTeste;

        public InserirTesteCommandHandler(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste ?? throw new ArgumentNullException(nameof(repositorioTeste));
        }

        public async Task<long> Handle(InserirTesteCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTeste.IncluirAsync(new Dominio.Entities.Teste(request.Descricao));
        }
    }
}
