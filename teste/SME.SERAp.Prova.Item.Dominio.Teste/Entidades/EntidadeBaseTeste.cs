using SME.SERAp.Prova.Item.Dominio.Entities;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class EntidadeBaseTeste
    {
        private class EntidadeBaseConcreta : EntidadeBase
        {
        }

        [Fact]
        public void Deve_Criar_EntidadeBase_Com_Id_Padrao()
        {
            var entidade = new EntidadeBaseConcreta();

            Assert.Equal(0, entidade.Id);
        }

        [Fact]
        public void Deve_Atribuir_Id_A_EntidadeBase()
        {
            var entidade = new EntidadeBaseConcreta
            {
                Id = 100
            };

            Assert.Equal(100, entidade.Id);
        }

        [Fact]
        public void Deve_Modificar_Id_Da_EntidadeBase()
        {
            var entidade = new EntidadeBaseConcreta
            {
                Id = 50
            };

            Assert.Equal(50, entidade.Id);

            entidade.Id = 200;

            Assert.Equal(200, entidade.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Zero()
        {
            var entidade = new EntidadeBaseConcreta
            {
                Id = 0
            };

            Assert.Equal(0, entidade.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Negativo()
        {
            var entidade = new EntidadeBaseConcreta
            {
                Id = -1
            };

            Assert.Equal(-1, entidade.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Positivo_Grande()
        {
            var entidade = new EntidadeBaseConcreta
            {
                Id = long.MaxValue
            };

            Assert.Equal(long.MaxValue, entidade.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Negativo_Grande()
        {
            var entidade = new EntidadeBaseConcreta
            {
                Id = long.MinValue
            };

            Assert.Equal(long.MinValue, entidade.Id);
        }

        [Fact]
        public void Deve_Herdar_EntidadeBase_Corretamente()
        {
            var entidade = new EntidadeBaseConcreta();

            Assert.IsAssignableFrom<EntidadeBase>(entidade);
        }
    }
}