using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using SME.SERAp.Prova.Item.Infra.Dtos.Audio;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using SME.SERAp.Prova.Item.Infra.Dtos.Video;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioArquivo : RepositorioBase<Arquivo>, IRepositorioArquivo
    {
        public RepositorioArquivo(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }
        public async Task<ArquivosItemDto> ObterArquivosAudioVideoPorItemId(long itemId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = new StringBuilder(@"

                                                 SELECT 
                                                    arquivoAudio.nome AS audioNome,
                                                    arquivoAudio.caminho AS audioCaminho,
                                                    arquivoVideo.nome AS videoNome,
                                                    arquivoVideo.caminho AS videoCaminho,
                                                    ia.id AS idAudio,
                                                    iv.id AS idVideo
                                                FROM ITEM i
                                                
                                                LEFT JOIN item_audio ia 
                                                    ON ia.item_id = i.id 
                                                    AND ia.arquivo_id = (
                                                        SELECT MAX(a.id)
                                                        FROM item_audio ia2
                                                        JOIN arquivo a ON a.id = ia2.arquivo_id
                                                        WHERE ia2.item_id = i.id
                                                    )
                                                
                                                LEFT JOIN arquivo arquivoAudio 
                                                    ON arquivoAudio.id = ia.arquivo_id
                                                
                                                LEFT JOIN item_video iv 
                                                    ON iv.item_id = i.id 
                                                    AND iv.arquivo_id = (
                                                        SELECT MAX(a.id)
                                                        FROM item_video iv2
                                                        JOIN arquivo a ON a.id = iv2.arquivo_id
                                                        WHERE iv2.item_id = i.id
                                                    )
                                                
                                                LEFT JOIN arquivo arquivoVideo 
                                                    ON arquivoVideo.id = iv.arquivo_id
                                                 
                                                ");
                if (itemId > 0)
                    query.Append($@" WHERE  i.id = @itemId;");
              
                
                return await conn.QueryFirstOrDefaultAsync<ArquivosItemDto>(query.ToString(), new { itemId });
            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<ItemAudioDto> ObterUltimoAudioPorItemId(long itemId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = new StringBuilder(@"
                                                SELECT
                                                    ia.id,
                                                    ia.arquivo_id as ArquivoId,
                                                    a.nome AS NomeArquivo,
                                                    a.content_type AS ContentType,
                                                    a.caminho
                                                FROM
                                                    item_audio ia
                                                INNER JOIN
                                                    arquivo a ON ia.arquivo_id = a.id
                                                WHERE
                                                    ia.item_id = @itemId
                                                    AND ia.situacao = 1
                                                ORDER BY
                                                    ia.criado_em DESC
                                                LIMIT 1;
                ");

                return await conn.QueryFirstOrDefaultAsync<ItemAudioDto>(query.ToString(), new { itemId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<ItemVideoDto> ObterUltimoVideoPorItemId(long itemId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = new StringBuilder(@"
                                                SELECT
                                                    iv.id,
                                                    iv.arquivo_id AS ArquivoId,
                                                    a.nome AS NomeArquivo,
                                                    a.content_type AS ContentType,
                                                    a.caminho
                                                FROM
                                                    item_video iv
                                                INNER JOIN
                                                    arquivo a ON iv.arquivo_id = a.id
                                                WHERE
                                                    iv.item_id = @itemId
                                                    AND iv.situacao = 1
                                                ORDER BY
                                                    iv.criado_em DESC
                                                LIMIT 1;
                ");

                return await conn.QueryFirstOrDefaultAsync<ItemVideoDto>(query.ToString(), new { itemId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}