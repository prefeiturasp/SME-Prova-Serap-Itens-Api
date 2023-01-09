using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AutenticacaoRevalidarUseCase : AbstractUseCase, IAutenticacaoRevalidarUseCase
    {
        public AutenticacaoRevalidarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<AutenticacaoRetornoDto> Executar(AutenticacaoRevalidarDto autenticacaoRevalidarDto)
        {
            var usuarioPermissaoDto = await mediator.Send(new ObterInformacoesPorTokenJwtQuery(autenticacaoRevalidarDto.Token));

            if (usuarioPermissaoDto == null)
                throw new NaoAutorizadoException("Token inválido", 401);

            return await mediator.Send(new ObterTokenJwtQuery(usuarioPermissaoDto));
        }
    }
}
