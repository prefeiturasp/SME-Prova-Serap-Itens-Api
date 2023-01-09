using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IAutenticacaoRevalidarUseCase
    {
        Task<AutenticacaoRetornoDto> Executar(AutenticacaoRevalidarDto autenticacaoRevalidarDto);
    }
}
