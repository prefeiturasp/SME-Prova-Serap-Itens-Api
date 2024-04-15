using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioQuantidadeAlternativas : IRepositorioBase<QuantidadeAlternativas>
    {
        Task<IEnumerable<QuantidadeAlternativas>> ObterQuantidadeAlternativas();


        Task<IEnumerable<QuantidadeAlternativasDto>> ObterListaQuantidadeAlternativas();
    }
}