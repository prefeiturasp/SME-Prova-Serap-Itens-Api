using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class DisciplinaMap : DommelEntityMap<Disciplina>
    {
        public DisciplinaMap()
        {
            ToTable("disciplina");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.AreaConhecimentoId).ToColumn("area_conhecimento_id");
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.NivelEnsino).ToColumn("nivel_ensino");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.Codigo).ToColumn("codigo");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
