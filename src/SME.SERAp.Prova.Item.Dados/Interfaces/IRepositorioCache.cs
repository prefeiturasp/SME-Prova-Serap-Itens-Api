using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioCache
    {
        Task<T> ObterRedisAsync<T>(string nomeChave, Func<Task<T>> buscarDados, int minutosParaExpirar = 720);
        Task<T> ObterRedisAsync<T>(string nomeChave);
        Task RemoverRedisAsync(string nomeChave);
        Task SalvarRedisAsync(string nomeChave, object valor, int minutosParaExpirar = 720);
    }
}
