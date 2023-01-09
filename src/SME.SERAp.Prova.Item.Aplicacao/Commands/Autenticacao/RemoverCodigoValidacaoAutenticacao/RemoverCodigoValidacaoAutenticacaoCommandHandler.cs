using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Cache;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class RemoverCodigoValidacaoAutenticacaoCommandHandler : IRequestHandler<RemoverCodigoValidacaoAutenticacaoCommand, bool>
    {
        private readonly IRepositorioCache repositorioCache;

        public RemoverCodigoValidacaoAutenticacaoCommandHandler(IRepositorioCache repositorioCache)
        {
            this.repositorioCache = repositorioCache ?? throw new ArgumentNullException(nameof(repositorioCache));
        }

        public async Task<bool> Handle(RemoverCodigoValidacaoAutenticacaoCommand request, CancellationToken cancellationToken)
        {
            var chave = CacheChave.ObterChave(CacheChave.Autenticacao, request.Codigo);
            await repositorioCache.RemoverRedisAsync(chave);
            return true;
        }
    }
}
