using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Mappings
{
    public class MatrizMap : DommelEntityMap<Matriz>
    {
        public MatrizMap()
        {
            ToTable("matriz");

            Map(c => c.Id).ToColumn("id").IsKey();
            Map(c => c.LegadoId).ToColumn("legado_id");
            Map(c => c.DisciplinaId).ToColumn("disciplina_id");
            Map(c => c.Descricao).ToColumn("descricao");
            Map(c => c.CriadoEm).ToColumn("criado_em");
            Map(c => c.AlteradoEm).ToColumn("alterado_em");
            Map(c => c.Status).ToColumn("status");
        }
    }
}
