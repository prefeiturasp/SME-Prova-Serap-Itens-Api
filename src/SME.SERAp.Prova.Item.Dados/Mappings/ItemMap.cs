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
            Map(c => c.AreaconhecimentoId).ToColumn("area_conhecimento_id");
            Map(c => c.DisciplinaId).ToColumn("disciplina_id");
            Map(c => c.MatrizId).ToColumn("matriz_id");
            Map(c => c.CompetenciaId).ToColumn("competencia_id");
            Map(c => c.HabilidadeId).ToColumn("habilidade_id");
            Map(c => c.AnoMatrizId).ToColumn("tipo_grade_id");
            Map(c => c.DificuldadeSugeridaId).ToColumn("dificuldade_sugerida_id");
            Map(c => c.Discriminacao).ToColumn("discriminacao");
            Map(c => c.AcertoCasual).ToColumn("acerto_casual");
            Map(c => c.Dificuldade).ToColumn("dificuldade");
        }
    }
}
