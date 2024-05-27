using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioItemAudio : RepositorioBase<ItemAudio>, IRepositorioItemAudio
    {
        public RepositorioItemAudio(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }
    }
}
