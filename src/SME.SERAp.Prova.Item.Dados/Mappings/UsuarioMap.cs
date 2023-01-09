using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Dados
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("usuario");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.Login).ToColumn("login");
            Map(c => c.Nome).ToColumn("nome");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
