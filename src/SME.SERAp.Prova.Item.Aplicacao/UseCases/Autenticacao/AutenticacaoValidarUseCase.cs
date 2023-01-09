using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AutenticacaoValidarUseCase : AbstractUseCase, IAutenticacaoValidarUseCase
    {
        public AutenticacaoValidarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<AutenticacaoRetornoDto> Executar(AutenticacaoValidarDto autenticacaoValidarDto)
        {
            var usuarioPermissaoDto = await mediator.Send(new ObterPermissaoUsuarioPorCodigoValidacaoQuery(autenticacaoValidarDto.Codigo));

            if (usuarioPermissaoDto == null)
                throw new NaoAutorizadoException("Código inválido", 401);

            await mediator.Send(new RemoverCodigoValidacaoAutenticacaoCommand(autenticacaoValidarDto.Codigo));

            return await mediator.Send(new ObterTokenJwtQuery(usuarioPermissaoDto));
        }
    }
}
