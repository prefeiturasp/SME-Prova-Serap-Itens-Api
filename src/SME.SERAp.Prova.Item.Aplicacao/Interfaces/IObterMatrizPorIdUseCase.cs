using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public interface IObterMatrizPorIdUseCase
    {
        Task<Matriz> Executar(long matrizId);
    }
}
