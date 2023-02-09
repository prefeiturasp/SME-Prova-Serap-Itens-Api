using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class ItemMap : DommelEntityMap<Dominio.Entities.Item>
    {
        public ItemMap()
        {
            ToTable("item");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.CodigoItem).ToColumn("codigo_item");
            Map(c => c.AreaconhecimentoId).ToColumn("are_conhecimento_legado_id");
            Map(c => c.DisciplinaId).ToColumn("disciplina_legado_id");
            Map(c => c.MatrizId).ToColumn("matriz_legado_id");
        }
    }
}
