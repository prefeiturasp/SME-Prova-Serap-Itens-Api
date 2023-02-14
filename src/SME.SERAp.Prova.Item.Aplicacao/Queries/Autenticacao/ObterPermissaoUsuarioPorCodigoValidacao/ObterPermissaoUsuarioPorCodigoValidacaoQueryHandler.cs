using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Cache;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterPermissaoUsuarioPorCodigoValidacaoQueryHandler : IRequestHandler<ObterPermissaoUsuarioPorCodigoValidacaoQuery, UsuarioPermissaoDto>
    {
        private readonly IRepositorioCache repositorioCache;

        public ObterPermissaoUsuarioPorCodigoValidacaoQueryHandler(IRepositorioCache repositorioCache)
        {
            this.repositorioCache = repositorioCache ?? throw new ArgumentNullException(nameof(repositorioCache));
        }

        public async Task<UsuarioPermissaoDto> Handle(ObterPermissaoUsuarioPorCodigoValidacaoQuery request, CancellationToken cancellationToken)
        {
            var chave = CacheChave.ObterChave(CacheChave.Autenticacao, request.Codigo);
            return await repositorioCache.ObterRedisAsync<UsuarioPermissaoDto>(chave);
        }
    }
}
