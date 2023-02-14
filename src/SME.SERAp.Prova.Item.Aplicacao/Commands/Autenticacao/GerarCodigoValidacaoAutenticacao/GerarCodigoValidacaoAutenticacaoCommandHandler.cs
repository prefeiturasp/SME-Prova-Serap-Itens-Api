using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Cache;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class GerarCodigoValidacaoAutenticacaoCommandHandler : IRequestHandler<GerarCodigoValidacaoAutenticacaoCommand, AutenticacaoValidarDto>
    {
        private readonly IRepositorioCache repositorioCache;

        public GerarCodigoValidacaoAutenticacaoCommandHandler(IRepositorioCache repositorioCache)
        {
            this.repositorioCache = repositorioCache ?? throw new ArgumentNullException(nameof(repositorioCache));
        }

        public async Task<AutenticacaoValidarDto> Handle(GerarCodigoValidacaoAutenticacaoCommand request, CancellationToken cancellationToken)
        {
            var codigo = Guid.NewGuid();
            var chave = CacheChave.ObterChave(CacheChave.Autenticacao, codigo);

            await repositorioCache.SalvarRedisAsync(chave, request.UsuarioPermissaoDto);

            return new AutenticacaoValidarDto(codigo.ToString());
        }
    }
}
