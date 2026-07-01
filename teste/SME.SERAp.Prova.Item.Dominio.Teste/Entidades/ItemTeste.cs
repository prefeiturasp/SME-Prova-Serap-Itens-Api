using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ItemTeste
    {
        private readonly string CodigoItemValido = "IT-12345";
        private readonly long AreaConhecimentoIdValida = 10;
        private readonly long DisciplinaIdValida = 20;
        private readonly long VersaoItemValida = 2;
        private readonly DateTime DataCriacaoSimulada = new DateTime(2025, 10, 27, 10, 0, 0);
        private readonly DateTime DataAlteracaoSimulada = new DateTime(2025, 10, 27, 10, 0, 0);
        private readonly string TextoBaseValido = "Texto de apoio para o item.";
        private readonly string FonteValida = "MEC/INEP";
        private readonly string EnunciadoValido = "Qual a capital do Brasil?";

        private DominioItem CriarItemComParametros()
        {
            return new DominioItem(
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
                textoBase: TextoBaseValido,
                fonte: FonteValida,
                enunciado: EnunciadoValido
            );
        }

        private void AssertarPropriedadesComuns(DominioItem item, long idEsperado, DateTime dataCriacaoEsperada, DateTime dataAlteracaoEsperada)
        {
            Assert.Equal(idEsperado, item.Id);
            Assert.Equal(CodigoItemValido, item.CodigoItem);
            Assert.Equal(AreaConhecimentoIdValida, item.AreaconhecimentoId);
            Assert.Equal(DisciplinaIdValida, item.DisciplinaId);
            Assert.Equal(VersaoItemValida, item.VersaoItem);
            Assert.Equal(dataCriacaoEsperada, item.DataCriacao);
            Assert.Equal(dataAlteracaoEsperada, item.DataAlteracao);
            Assert.Equal(TextoBaseValido, item.TextoBase);
            Assert.Equal(FonteValida, item.Fonte);
            Assert.Equal(EnunciadoValido, item.Enunciado);
            Assert.Equal(SituacaoItem.Ativo, item.Situacao);
            Assert.Equal(TipoItem.Dicotômico, item.Tipo);
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
                DataCriacao = DataCriacaoSimulada,
                DataAlteracao = DataAlteracaoSimulada,
                Enunciado = EnunciadoValido
            };

            Assert.Equal(idSimulado, item.Id);
            Assert.Equal(CodigoItemValido, item.CodigoItem);
            Assert.Equal(VersaoItemValida, item.VersaoItem);
            Assert.Equal(DataCriacaoSimulada, item.DataCriacao);
            Assert.Equal(DataAlteracaoSimulada, item.DataAlteracao);
            Assert.Equal(EnunciadoValido, item.Enunciado);

            Assert.Equal(0, item.AreaconhecimentoId);
            Assert.Null(item.MatrizId);
        }

        [Fact]
        public void Deve_Criar_Novo_Item_Com_Construtor_Parametros_E_ID_Zero_E_Datas_Padrao()
        {
            var item = CriarItemComParametros();

            AssertarPropriedadesComuns(item, idEsperado: 0, dataCriacaoEsperada: default(DateTime), dataAlteracaoEsperada: default(DateTime));
        }

        [Fact]
        public void Deve_Criar_Item_E_Atribuir_ID_E_Datas_Posteriormente()
        {
            long idSimulado = 123;
            var item = CriarItemComParametros();

            item.Id = idSimulado;
            item.DataCriacao = DataCriacaoSimulada;
            item.DataAlteracao = DataAlteracaoSimulada;

            AssertarPropriedadesComuns(item, idEsperado: idSimulado, dataCriacaoEsperada: DataCriacaoSimulada, dataAlteracaoEsperada: DataAlteracaoSimulada);
        }
    }
}