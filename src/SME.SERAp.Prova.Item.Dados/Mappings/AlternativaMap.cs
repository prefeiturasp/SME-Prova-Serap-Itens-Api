using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class AlternativaMap : DommelEntityMap<Dominio.Entities.Alternativa>
    {
        public AlternativaMap()
        {
            ToTable("alternativa");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.Ordem).ToColumn("ordem");
            Map(c => c.Numeracao).ToColumn("numeracao");
            Map(c => c.Justificativa).ToColumn("justificativa");
            Map(c => c.Correta).ToColumn("correta");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.ItemId).ToColumn("item_id");

        }
    }
}
