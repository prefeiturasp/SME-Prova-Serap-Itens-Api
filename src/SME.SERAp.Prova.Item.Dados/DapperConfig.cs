using Dapper;
using Dommel;
using SME.SERAp.Prova.Item.Dados.TypeMappers;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dados
{
    public static class DapperConfig
    {
        public static void RegistrarMapeamentos()
        {
            // ── Dapper puro (QueryAsync, ExecuteAsync, etc.) ─────────────────
            SqlMapper.SetTypeMap(typeof(Alternativa), new ColumnAttributeTypeMapper<Alternativa>());
            SqlMapper.SetTypeMap(typeof(AreaConhecimento), new ColumnAttributeTypeMapper<AreaConhecimento>());
            SqlMapper.SetTypeMap(typeof(Arquivo), new ColumnAttributeTypeMapper<Arquivo>());
            SqlMapper.SetTypeMap(typeof(Assunto), new ColumnAttributeTypeMapper<Assunto>());
            SqlMapper.SetTypeMap(typeof(Competencia), new ColumnAttributeTypeMapper<Competencia>());
            SqlMapper.SetTypeMap(typeof(Dificuldade), new ColumnAttributeTypeMapper<Dificuldade>());
            SqlMapper.SetTypeMap(typeof(Disciplina), new ColumnAttributeTypeMapper<Disciplina>());
            SqlMapper.SetTypeMap(typeof(Habilidade), new ColumnAttributeTypeMapper<Habilidade>());
            SqlMapper.SetTypeMap(typeof(Dominio.Entities.Item), new ColumnAttributeTypeMapper<Dominio.Entities.Item>());
            SqlMapper.SetTypeMap(typeof(ItemAudio), new ColumnAttributeTypeMapper<ItemAudio>());
            SqlMapper.SetTypeMap(typeof(ItemVideo), new ColumnAttributeTypeMapper<ItemVideo>());
            SqlMapper.SetTypeMap(typeof(Matriz), new ColumnAttributeTypeMapper<Matriz>());
            SqlMapper.SetTypeMap(typeof(NivelItem), new ColumnAttributeTypeMapper<NivelItem>());
            SqlMapper.SetTypeMap(typeof(QuantidadeAlternativas), new ColumnAttributeTypeMapper<QuantidadeAlternativas>());
            SqlMapper.SetTypeMap(typeof(SequencialItem), new ColumnAttributeTypeMapper<SequencialItem>());
            SqlMapper.SetTypeMap(typeof(SubAssunto), new ColumnAttributeTypeMapper<SubAssunto>());
            SqlMapper.SetTypeMap(typeof(TipoGrade), new ColumnAttributeTypeMapper<TipoGrade>());
            SqlMapper.SetTypeMap(typeof(Usuario), new ColumnAttributeTypeMapper<Usuario>());

            // ── Dommel (GetAsync, InsertAsync, UpdateAsync, DeleteAsync) ─────
            DommelMapper.SetColumnNameResolver(new ColumnAttributeColumnNameResolver());
            DommelMapper.SetTableNameResolver(new ColumnAttributeTableNameResolver());
            DommelMapper.SetKeyPropertyResolver(new ColumnAttributeKeyPropertyResolver());
        }
    }
}