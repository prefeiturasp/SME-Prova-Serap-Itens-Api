using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AutenticacaoUseCase : AbstractUseCase, IAutenticacaoUseCase
    {
        public AutenticacaoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<AutenticacaoValidarDto> Executar(AutenticacaoDto autenticacaoDto)
        {
            var grupoIdGuid = Guid.NewGuid();
            _ = Guid.TryParse(autenticacaoDto.Perfil, out grupoIdGuid);
            
            var usuarioPermissao = await mediator.Send(new ObterPermissaoUsuarioPorLoginGrupoIdQuery(autenticacaoDto.Login, grupoIdGuid));

            if (usuarioPermissao == null)
                throw new NaoAutorizadoException($"Usuário: {autenticacaoDto.Login} inválido com o grupo {autenticacaoDto.Perfil}.", 401);

            return await mediator.Send(new GerarCodigoValidacaoAutenticacaoCommand(usuarioPermissao));
        }
    }
}
