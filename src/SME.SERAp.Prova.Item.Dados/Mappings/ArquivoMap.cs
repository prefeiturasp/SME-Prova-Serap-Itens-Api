using Dapper.FluentMap.Dommel.Mapping;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class ArquivoMap : DommelEntityMap<Dominio.Entities.Arquivo>
    {
        public ArquivoMap()
        {
            ToTable("arquivo");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.Caminho).ToColumn("caminho");
            Map(c => c.ContentType).ToColumn("content_type");
            Map(c => c.Situacao).ToColumn("situacao");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
        }
    }
}
