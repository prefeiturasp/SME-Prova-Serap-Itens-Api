using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioDificuldade : IRepositorioBase<Dificuldade>
    {
        Task<Dificuldade> ObterPorLegadoIdAsync(long legadoId);
    }
}
