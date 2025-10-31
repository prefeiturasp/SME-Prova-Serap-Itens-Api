using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class ArquivoTeste
    {
        [Fact]
        public void Deve_Criar_Arquivo_Com_Construtor_Padrao()
        {
            var arquivo = new Arquivo
            {
                LegadoId = 100,
                Nome = "documento.pdf",
                Caminho = "/arquivos/2024/documento.pdf",
                ContentType = "application/pdf",
                Situacao = 1,
                CriadoEm = new DateTime(2024, 1, 15, 10, 30, 0),
                AlteradoEm = new DateTime(2024, 1, 20, 14, 45, 0)
            };

            Assert.Equal(100, arquivo.LegadoId);
            Assert.Equal("documento.pdf", arquivo.Nome);
            Assert.Equal("/arquivos/2024/documento.pdf", arquivo.Caminho);
            Assert.Equal("application/pdf", arquivo.ContentType);
            Assert.Equal(1, arquivo.Situacao);
            Assert.Equal(new DateTime(2024, 1, 15, 10, 30, 0), arquivo.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20, 14, 45, 0), arquivo.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Arquivo_Com_Construtor_Parametros()
        {
            var dataCriacao = new DateTime(2024, 2, 10, 8, 15, 30);

            var arquivo = new Arquivo(
                legadoId: 200,
                nome: "imagem.jpg",
                caminho: "/uploads/imagens/imagem.jpg",
                contentType: "image/jpeg",
                situacao: 1,
                criadoEm: dataCriacao
            );

            Assert.Equal(200, arquivo.LegadoId);
            Assert.Equal("imagem.jpg", arquivo.Nome);
            Assert.Equal("/uploads/imagens/imagem.jpg", arquivo.Caminho);
            Assert.Equal("image/jpeg", arquivo.ContentType);
            Assert.Equal(1, arquivo.Situacao);
            Assert.Equal(dataCriacao, arquivo.CriadoEm);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Como_Nulo_Quando_Id_Zero()
        {
            var dataCriacao = new DateTime(2024, 3, 5, 12, 0, 0);

            var arquivo = new Arquivo(
                legadoId: 300,
                nome: "planilha.xlsx",
                caminho: "/documentos/planilha.xlsx",
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                situacao: 1,
                criadoEm: dataCriacao
            );

            Assert.Equal(0, arquivo.Id);
            Assert.Null(arquivo.AlteradoEm);
        }

        [Fact]
        public void Deve_Manter_AlteradoEm_Quando_Id_Diferente_De_Zero()
        {
            var dataCriacao = new DateTime(2024, 4, 12, 9, 45, 0);
            var dataAlteracao = new DateTime(2024, 4, 15, 11, 20, 0);

            var arquivo = new Arquivo(
                legadoId: 400,
                nome: "video.mp4",
                caminho: "/media/videos/video.mp4",
                contentType: "video/mp4",
                situacao: 2,
                criadoEm: dataCriacao
            );

            arquivo.Id = 50;
            arquivo.AlteradoEm = dataAlteracao;

            Assert.Equal(50, arquivo.Id);
            Assert.Equal(dataAlteracao, arquivo.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Arquivo_Com_Diferentes_ContentTypes()
        {
            var dataCriacao = DateTime.Now;

            var arquivoPdf = new Arquivo(1, "doc.pdf", "/path/doc.pdf", "application/pdf", 1, dataCriacao);
            var arquivoImagem = new Arquivo(2, "foto.png", "/path/foto.png", "image/png", 1, dataCriacao);
            var arquivoTexto = new Arquivo(3, "texto.txt", "/path/texto.txt", "text/plain", 1, dataCriacao);

            Assert.Equal("application/pdf", arquivoPdf.ContentType);
            Assert.Equal("image/png", arquivoImagem.ContentType);
            Assert.Equal("text/plain", arquivoTexto.ContentType);
        }

        [Fact]
        public void Deve_Criar_Arquivo_Com_Diferentes_Situacoes()
        {
            var dataCriacao = DateTime.Now;

            var arquivoAtivo = new Arquivo(1, "ativo.pdf", "/path/ativo.pdf", "application/pdf", 1, dataCriacao);
            var arquivoInativo = new Arquivo(2, "inativo.pdf", "/path/inativo.pdf", "application/pdf", 0, dataCriacao);

            Assert.Equal(1, arquivoAtivo.Situacao);
            Assert.Equal(0, arquivoInativo.Situacao);
        }
    }
}