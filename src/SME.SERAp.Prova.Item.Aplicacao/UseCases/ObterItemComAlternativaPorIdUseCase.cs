using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Arquivo.ObterUltimoAudioPorItemId;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Arquivo.ObterUltimoVideoPorItemId;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemComAlternativaPorId;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterItemComAlternativaPorIdUseCase : AbstractUseCase, IObterItemComAlternativaPorIdUseCase
    {
        public ObterItemComAlternativaPorIdUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<ItemComAlternativasDto> Executar(long itemId)
        {
            var item = await mediator.Send(new ObterItemComAlternativaPorIdQuery(itemId));

            if (item == null)
            {
                return null;
            }

            var audioDto = await mediator.Send(new ObterUltimoAudioPorItemIdQuery(itemId));
            var videoDto = await mediator.Send(new ObterUltimoVideoPorItemIdQuery(itemId));

            var itemDetalhe = new ItemComAlternativasDto
            {

                Id = item.Id,
                VersaoItem = item.VersaoItem,
                CodigoItem = item.CodigoItem,
                AreaconhecimentoId = item.AreaconhecimentoId,
                DisciplinaId = item.DisciplinaId,
                MatrizId = item.MatrizId,
                CompetenciaId = item.CompetenciaId,
                HabilidadeId = item.HabilidadeId,
                AnoMatrizId = item.AnoMatrizId,
                DificuldadeSugeridaId = item.DificuldadeSugeridaId,
                Discriminacao = item.Discriminacao,
                AcertoCasual = item.AcertoCasual,
                Dificuldade = item.Dificuldade,
                AssuntoId = item.AssuntoId,
                SubAssuntoId = item.SubAssuntoId,
                Situacao = item.Situacao,
                Tipo = item.Tipo,
                QuantidadeAlternativasId = item.QuantidadeAlternativasId,
                PalavrasChave = item.PalavrasChave,
                ParametroBTransformado = item.ParametroBTransformado,
                MediaEhDesvio = item.MediaEhDesvio,
                Observacao = item.Observacao,
                SentencaDescritora = item.SentencaDescritora,
                NivelItem = item.NivelItem,
                DataCriacao = item.DataCriacao,
                DataAlteracao = item.DataAlteracao,
                TextoBase = item.TextoBase,
                Fonte = item.Fonte,
                Enunciado = item.Enunciado,

                Alternativas = item.Alternativas,

                Video = videoDto,
                Audio = audioDto
            };

            return itemDetalhe;
        }
    }
}