﻿using Dapper.FluentMap.Dommel.Mapping;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class ItemVideoMap : DommelEntityMap<Dominio.Entities.ItemVideo>
    {
        public ItemVideoMap()
        {
            ToTable("item_video");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.ArquivoId).ToColumn("arquivo_id");
            Map(c => c.ItemId).ToColumn("item_id");
            Map(c => c.Situacao).ToColumn("situacao");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
        }
    }
}