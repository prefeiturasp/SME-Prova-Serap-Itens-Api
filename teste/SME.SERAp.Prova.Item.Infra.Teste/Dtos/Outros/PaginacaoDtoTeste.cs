//using SME.SERAp.Prova.Item.Infra.Dtos;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Outros
//{
//    public class PaginacaoDtoTeste
//    {
//        [Fact]
//        public void Deve_Atribuir_Propriedades_Corretamente()
//        {
//            var itens = new List<string> { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };
//            var pagina = 1;
//            var tamanhoPagina = 10;
//            var totalRegistros = 50;

//            var dto = new PaginacaoDto<string>(itens, pagina, tamanhoPagina, totalRegistros);

//            Assert.Equal(itens, dto.Itens);
//            Assert.Equal(pagina, dto.Pagina);
//            Assert.Equal(tamanhoPagina, dto.TamanhoPagina);
//            Assert.Equal(totalRegistros, dto.TotalRegistros);
//            Assert.Equal(5, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Calcular_TotalPaginas_Corretamente_Quando_Divisao_Exata()
//        {
//            var itens = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//            var totalRegistros = 100;
//            var tamanhoPagina = 10;

//            var dto = new PaginacaoDto<int>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(10, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Calcular_TotalPaginas_Corretamente_Quando_Divisao_Nao_Exata()
//        {
//            var itens = new List<int> { 1, 2, 3, 4, 5 };
//            var totalRegistros = 23;
//            var tamanhoPagina = 5;

//            var dto = new PaginacaoDto<int>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(5, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Lista_Vazia()
//        {
//            var itens = new List<string>();
//            var pagina = 1;
//            var tamanhoPagina = 10;
//            var totalRegistros = 0;

//            var dto = new PaginacaoDto<string>(itens, pagina, tamanhoPagina, totalRegistros);

//            Assert.Empty(dto.Itens);
//            Assert.Equal(1, dto.Pagina);
//            Assert.Equal(10, dto.TamanhoPagina);
//            Assert.Equal(0, dto.TotalRegistros);
//            Assert.Equal(0, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Um_Registro()
//        {
//            var itens = new List<string> { "Único item" };
//            var totalRegistros = 1;
//            var tamanhoPagina = 10;

//            var dto = new PaginacaoDto<string>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Single(dto.Itens);
//            Assert.Equal(1, dto.TotalRegistros);
//            Assert.Equal(1, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Primeira_Pagina()
//        {
//            var itens = new List<int> { 1, 2, 3, 4, 5 };
//            var pagina = 1;
//            var tamanhoPagina = 5;
//            var totalRegistros = 50;

//            var dto = new PaginacaoDto<int>(itens, pagina, tamanhoPagina, totalRegistros);

//            Assert.Equal(1, dto.Pagina);
//            Assert.Equal(10, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Pagina_Intermediaria()
//        {
//            var itens = new List<int> { 11, 12, 13, 14, 15 };
//            var pagina = 3;
//            var tamanhoPagina = 5;
//            var totalRegistros = 50;

//            var dto = new PaginacaoDto<int>(itens, pagina, tamanhoPagina, totalRegistros);

//            Assert.Equal(3, dto.Pagina);
//            Assert.Equal(10, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Ultima_Pagina()
//        {
//            var itens = new List<int> { 46, 47, 48, 49, 50 };
//            var pagina = 10;
//            var tamanhoPagina = 5;
//            var totalRegistros = 50;

//            var dto = new PaginacaoDto<int>(itens, pagina, tamanhoPagina, totalRegistros);

//            Assert.Equal(10, dto.Pagina);
//            Assert.Equal(10, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Ultima_Pagina_Incompleta()
//        {
//            var itens = new List<int> { 21, 22, 23 };
//            var pagina = 5;
//            var tamanhoPagina = 5;
//            var totalRegistros = 23;

//            var dto = new PaginacaoDto<int>(itens, pagina, tamanhoPagina, totalRegistros);

//            Assert.Equal(3, dto.Itens.Count());
//            Assert.Equal(5, dto.Pagina);
//            Assert.Equal(5, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Tamanho_Pagina_10()
//        {
//            var itens = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
//            var totalRegistros = 100;
//            var tamanhoPagina = 10;

//            var dto = new PaginacaoDto<string>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(10, dto.TamanhoPagina);
//            Assert.Equal(10, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Tamanho_Pagina_25()
//        {
//            var itens = new List<int>(Enumerable.Range(1, 25));
//            var totalRegistros = 100;
//            var tamanhoPagina = 25;

//            var dto = new PaginacaoDto<int>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(25, dto.TamanhoPagina);
//            Assert.Equal(4, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Tamanho_Pagina_50()
//        {
//            var itens = new List<int>(Enumerable.Range(1, 50));
//            var totalRegistros = 100;
//            var tamanhoPagina = 50;

//            var dto = new PaginacaoDto<int>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(50, dto.TamanhoPagina);
//            Assert.Equal(2, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Com_Tamanho_Pagina_100()
//        {
//            var itens = new List<int>(Enumerable.Range(1, 100));
//            var totalRegistros = 100;
//            var tamanhoPagina = 100;

//            var dto = new PaginacaoDto<int>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(100, dto.TamanhoPagina);
//            Assert.Equal(1, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Tipo_String()
//        {
//            var itens = new List<string> { "Texto 1", "Texto 2", "Texto 3" };

//            var dto = new PaginacaoDto<string>(itens, 1, 10, 30);

//            Assert.IsType<PaginacaoDto<string>>(dto);
//            Assert.All(dto.Itens, item => Assert.IsType<string>(item));
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Tipo_Int()
//        {
//            var itens = new List<int> { 1, 2, 3, 4, 5 };

//            var dto = new PaginacaoDto<int>(itens, 1, 10, 50);

//            Assert.IsType<PaginacaoDto<int>>(dto);
//            Assert.All(dto.Itens, item => Assert.IsType<int>(item));
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Tipo_Long()
//        {
//            var itens = new List<long> { 100L, 200L, 300L };

//            var dto = new PaginacaoDto<long>(itens, 1, 10, 30);

//            Assert.IsType<PaginacaoDto<long>>(dto);
//            Assert.All(dto.Itens, item => Assert.IsType<long>(item));
//        }

//        [Fact]
//        public void Deve_Criar_PaginacaoDto_Tipo_Objeto_Complexo()
//        {
//            var itens = new List<ItemTeste>
//            {
//                new ItemTeste { Id = 1, Nome = "Item 1" },
//                new ItemTeste { Id = 2, Nome = "Item 2" },
//                new ItemTeste { Id = 3, Nome = "Item 3" }
//            };

//            var dto = new PaginacaoDto<ItemTeste>(itens, 1, 10, 30);

//            Assert.IsType<PaginacaoDto<ItemTeste>>(dto);
//            Assert.Equal(3, dto.Itens.Count());
//            Assert.Equal("Item 1", dto.Itens.First().Nome);
//        }

//        [Fact]
//        public void Deve_Calcular_TotalPaginas_Um_Quando_TotalRegistros_Menor_Que_TamanhoPagina()
//        {
//            var itens = new List<string> { "A", "B", "C" };
//            var totalRegistros = 3;
//            var tamanhoPagina = 10;

//            var dto = new PaginacaoDto<string>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(1, dto.TotalPaginas);
//        }

//        [Fact]
//        public void Deve_Ter_Propriedades_Somente_Leitura()
//        {
//            var itens = new List<int> { 1, 2, 3 };
//            var dto = new PaginacaoDto<int>(itens, 1, 10, 30);

//            var propriedadeItens = typeof(PaginacaoDto<int>).GetProperty(nameof(dto.Itens));
//            var propriedadePagina = typeof(PaginacaoDto<int>).GetProperty(nameof(dto.Pagina));
//            var propriedadeTamanhoPagina = typeof(PaginacaoDto<int>).GetProperty(nameof(dto.TamanhoPagina));
//            var propriedadeTotalRegistros = typeof(PaginacaoDto<int>).GetProperty(nameof(dto.TotalRegistros));
//            var propriedadeTotalPaginas = typeof(PaginacaoDto<int>).GetProperty(nameof(dto.TotalPaginas));

//            Assert.False(propriedadeItens.CanWrite);
//            Assert.False(propriedadePagina.CanWrite);
//            Assert.False(propriedadeTamanhoPagina.CanWrite);
//            Assert.False(propriedadeTotalRegistros.CanWrite);
//            Assert.False(propriedadeTotalPaginas.CanWrite);
//        }

//        [Fact]
//        public void Deve_Calcular_TotalPaginas_Corretamente_Com_Numero_Grande()
//        {
//            var itens = new List<int>(Enumerable.Range(1, 10));
//            var totalRegistros = 1000;
//            var tamanhoPagina = 10;

//            var dto = new PaginacaoDto<int>(itens, 1, tamanhoPagina, totalRegistros);

//            Assert.Equal(100, dto.TotalPaginas);
//        }

//        // Classe auxiliar para teste com objeto complexo
//        private class ItemTeste
//        {
//            public int Id { get; set; }
//            public string Nome { get; set; }
//        }
//    }
//}