using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
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

                                                SELECT arquivoAudio.nome as audioNome , 
                                                        arquivoAudio.caminho  as audioCaminho,
                                                       arquivoVideo.nome  as videoNome, 
                                                       arquivoVideo.caminho as videoCaminho,
                                                       ia.id as idAudio,
                                                       iv.id as idVideo
                                                FROM ITEM  i
                                                LEFT JOIN item_audio ia on  i.id = ia.item_id 
                                                LEFT JOIN item_video iv on  i.id = iv.item_id 
                                                LEFT JOIN arquivo  arquivoVideo on arquivoVideo.id = iv.arquivo_id
                                                LEFT JOIN arquivo  arquivoAudio on arquivoAudio.id = ia.arquivo_id
                                                 
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
    }
}

