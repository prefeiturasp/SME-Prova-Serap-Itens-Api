using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IInserirTesteUseCase
    {
        Task<long> Executar(string descricao);
    }
}
