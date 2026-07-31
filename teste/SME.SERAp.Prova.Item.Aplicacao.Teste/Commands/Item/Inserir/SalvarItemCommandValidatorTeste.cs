using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Item
{
    public class SalvarItemCommandValidatorTeste
    {
        private readonly SalvarItemCommandValidator validator;

        public SalvarItemCommandValidatorTeste()
        {
            validator = new SalvarItemCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_AreaConhecimentoId_For_Zero()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 0,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Rascunho
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "Item.AreaconhecimentoId" &&
                e.ErrorMessage == "A Area de Conhecimento precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_DisciplinaId_For_Zero()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 0,
                Situacao = SituacaoItem.Rascunho
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "Item.DisciplinaId" &&
                e.ErrorMessage == "A Disciplina precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_Observacao_Ultrapassar_100_Caracteres()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Rascunho,
                Observacao = new string('A', 101)
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A observação pode ter no máximo 100 caracteres.");
        }

        [Fact]
        public void Deve_Falhar_Quando_SentencaDescritora_Ultrapassar_100_Caracteres()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Rascunho,
                SentencaDescritora = new string('B', 101)
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A Sentença Descritora pode ter no máximo 100 caracteres.");
        }

        [Fact]
        public void Deve_Passar_Quando_Situacao_For_Rascunho_E_Campos_De_Item_Ausentes()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Rascunho
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Deve_Falhar_Quando_MatrizId_For_Zero_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 0,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A Matriz precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_CompetenciaId_For_Nula_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = null,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A Competencia precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_HabilidadeId_For_Nula_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = null,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A Habilidade precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_AnoMatrizId_For_Nulo_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = null,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "O AnoMatriz precisa ser informado.");
        }

        [Fact]
        public void Deve_Falhar_Quando_DificuldadeSugeridaId_For_Nula_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = null,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A Dificuldade Sugerida precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_QuantidadeAlternativasId_For_Zero_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 0,
                PalavrasChave = "teste",
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "A Quantidade de Alternativas precisa ser informada.");
        }

        [Fact]
        public void Deve_Falhar_Quando_PalavrasChave_For_Nula_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = null,
                Enunciado = "Teste"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "É necessário informar pelo menos uma palavra chave.");
        }

        [Fact]
        public void Deve_Falhar_Quando_Enunciado_For_Nulo_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = null
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == "É necessário informar o enunciado do Item.");
        }

        [Fact]
        public void Deve_Passar_Quando_Todos_Os_Campos_Obrigatorios_Forem_Validos_E_Situacao_Ativo()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 1,
                Situacao = SituacaoItem.Ativo,
                MatrizId = 1,
                CompetenciaId = 1,
                HabilidadeId = 1,
                AnoMatrizId = 1,
                DificuldadeSugeridaId = 1,
                QuantidadeAlternativasId = 1,
                PalavrasChave = "teste",
                Enunciado = "Enunciado válido"
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}