using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class QuantidadeAlternativaMap : DommelEntityMap<QuantidadeAlternativas>
    {
        public QuantidadeAlternativaMap()
        {
            ToTable("quantidade_alternativa");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.EhPadrao).ToColumn("eh_padrao");
            Map(c => c.QtdAlternativas).ToColumn("qtde_alternativa");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}


