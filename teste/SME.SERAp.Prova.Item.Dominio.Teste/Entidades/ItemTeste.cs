using System;
using Xunit;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using System.Linq;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ItemTeste
    {
        private readonly string CodigoItemValido = "IT-12345";
        private readonly long AreaConhecimentoIdValida = 10;
        private readonly long DisciplinaIdValida = 20;
        private readonly long VersaoItemValida = 2;
        private readonly DateTime DataCriacaoValida = new DateTime(2025, 10, 27, 10, 0, 0);
        private readonly string TextoBaseValido = "Texto de apoio para o item.";
        private readonly string FonteValida = "MEC/INEP";
        private readonly string EnunciadoValido = "Qual a capital do Brasil?";

        private DominioItem CriarItemComParametros(long? id)
        {
            return new DominioItem(
                id: id,
                codigoItem: CodigoItemValido,
                areaconhecimentoId: AreaConhecimentoIdValida,
                disciplinaId: DisciplinaIdValida,
                matrizId: 100,
                competenciaId: 200,
                habilidadeId: 300,
                anoMatrizId: 2024,
                dificuldadeSugeridaId: 1,
                discriminacao: 0.8M,
                acertoCasual: 0.2M,
                dificuldade: 0.5M,
                assuntoId: 400,
                subassuntoId: 401,
                situacao: SituacaoItem.Ativo,
                tipo: TipoItem.Dicotômico,
                quantidadeAlternativaId: 5,
                palavrasChave: "capital,brasil,geografia",
                parametroBTransformado: -1.2M,
                mediaEhDesvio: "0.5;0.1",
                observacao: "Nenhuma observação.",
                sentencaDescritora: "Item de nível fácil.",
                nivelItem: 3.5M,
                versaoItem: VersaoItemValida,
                dataCriacao: DataCriacaoValida,
                textoBase: TextoBaseValido,
                fonte: FonteValida,
                enunciado: EnunciadoValido
            );
        }

        private void AssertarPropriedadesComuns(DominioItem item, long idEsperado, long? idEntrada, bool deveSetarDataAlteracao)
        {
            Assert.Equal(idEsperado, item.Id);
            Assert.Equal(CodigoItemValido, item.CodigoItem);
            Assert.Equal(AreaConhecimentoIdValida, item.AreaconhecimentoId);
            Assert.Equal(DisciplinaIdValida, item.DisciplinaId);
            Assert.Equal(VersaoItemValida, item.VersaoItem);
            Assert.Equal(DataCriacaoValida, item.DataCriacao);
            Assert.Equal(TextoBaseValido, item.TextoBase);
            Assert.Equal(FonteValida, item.Fonte);
            Assert.Equal(EnunciadoValido, item.Enunciado);
            Assert.Equal(SituacaoItem.Ativo, item.Situacao);
            Assert.Equal(TipoItem.Dicotômico, item.Tipo);

            if (deveSetarDataAlteracao)
            {
                Assert.NotEqual(default(DateTime), item.DataAlteracao);
            }
            else
            {
                Assert.Equal(default(DateTime), item.DataAlteracao);
            }
        }

        [Fact]
        public void Deve_Criar_Item_Com_Construtor_Padrao_E_Atribuicao_Simples()
        {
            var idSimulado = 55;

            var item = new DominioItem
            {
                Id = idSimulado,
                CodigoItem = CodigoItemValido,
                VersaoItem = VersaoItemValida,
                DataCriacao = DataCriacaoValida,
                Enunciado = EnunciadoValido
            };

            Assert.Equal(idSimulado, item.Id);
            Assert.Equal(CodigoItemValido, item.CodigoItem);
            Assert.Equal(VersaoItemValida, item.VersaoItem);
            Assert.Equal(DataCriacaoValida, item.DataCriacao);
            Assert.Equal(EnunciadoValido, item.Enunciado);

            Assert.Equal(0, item.AreaconhecimentoId);
            Assert.Null(item.MatrizId);
        }

        [Fact]
        public void Deve_Criar_Novo_Item_Com_Construtor_Parametros_E_ID_Null()
        {
            var item = CriarItemComParametros(id: null);

            AssertarPropriedadesComuns(item, idEsperado: 0, idEntrada: null, deveSetarDataAlteracao: false);
        }

        [Fact]
        public void Deve_Criar_Novo_Item_Com_Construtor_Parametros_E_ID_Zero()
        {
            var item = CriarItemComParametros(id: 0);

            AssertarPropriedadesComuns(item, idEsperado: 0, idEntrada: 0, deveSetarDataAlteracao: false);
        }

        [Fact]
        public void Deve_Criar_Item_Existente_Com_Construtor_Parametros_E_ID_Valido()
        {
            long idExistente = 123;
            var tempoAntesDaChamada = DateTime.Now;

            var item = CriarItemComParametros(id: idExistente);

            AssertarPropriedadesComuns(item, idEsperado: idExistente, idEntrada: idExistente, deveSetarDataAlteracao: true);

            Assert.Equal(idExistente, item.Id);
            Assert.True(item.DataAlteracao >= tempoAntesDaChamada, "DataAlteracao deve ser maior ou igual ao tempo de criação do objeto.");
            Assert.NotEqual(default(DateTime), item.DataAlteracao);
        }
    }
}