using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IAtualizarSituacaoItemUseCase
    {
        Task<bool> Executar(string codigoItem, long versaoItem, int situacao);
    }
}