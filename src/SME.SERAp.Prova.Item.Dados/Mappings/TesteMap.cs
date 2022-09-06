using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class TesteMap : DommelEntityMap<Teste>
    {
        public TesteMap()
        {
            ToTable("teste");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.Descricao).ToColumn("descricao");
        }
    }
}
