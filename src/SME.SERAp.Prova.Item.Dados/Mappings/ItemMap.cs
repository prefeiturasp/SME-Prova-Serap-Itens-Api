using Dapper.FluentMap.Dommel.Mapping;
using SME.SERAp.Prova.Item.Dominio.Enums;
using System;

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
            Map(c => c.AssuntoId).ToColumn("assunto_id");
            Map(c => c.SubAssuntoId).ToColumn("subassunto_id");
            Map(c => c.QuantidadeAlternativasId).ToColumn("quantidade_alternativa_id");
            Map(c => c.Situacao).ToColumn("situacao");
            Map(c => c.Tipo).ToColumn("tipo");
            Map(c => c.PalavrasChave).ToColumn("palavras_chave");
            Map(c => c.ParametroBTransformado).ToColumn("parametro_b_transformado");
            Map(c => c.MediaEhDesvio).ToColumn("media_eh_desvio");
            Map(c => c.Observacao).ToColumn("observacao");
            Map(c => c.DataCriacao).ToColumn("criado_em");
            Map(c => c.DataAlteracao).ToColumn("alterado_em");
            Map(c => c.TextoBase).ToColumn("texto_base");
            Map(c => c.Fonte).ToColumn("fonte");
            Map(c => c.Enunciado).ToColumn("enunciado");
        }
    }
}
