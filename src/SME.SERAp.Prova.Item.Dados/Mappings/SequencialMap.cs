using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class SequencialMap : DommelEntityMap<SequencialItem>
    {
        public SequencialMap()
        {
            ToTable("sequencial_item");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.CodigoAreaConhecimento).ToColumn("codigo_area_conhecimento");
            Map(c => c.CodigoDisciplina).ToColumn("codigo_disciplina");
            Map(c => c.Sequencial).ToColumn("sequencial");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
        }
    }
}