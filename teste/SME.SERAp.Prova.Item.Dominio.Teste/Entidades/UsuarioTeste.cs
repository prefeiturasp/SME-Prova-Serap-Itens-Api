using System;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class UsuarioTeste
    {
        [Fact]
        public void Deve_Criar_Usuario_Com_Construtor_Padrao()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario
            {
                LegadoId = legadoId,
                Login = "joao.silva",
                Nome = "João Silva",
                CriadoEm = new DateTime(2024, 1, 15),
                AlteradoEm = new DateTime(2024, 1, 20),
                Status = StatusGeral.Ativo
            };

            Assert.Equal(legadoId, usuario.LegadoId);
            Assert.Equal("joao.silva", usuario.Login);
            Assert.Equal("João Silva", usuario.Nome);
            Assert.Equal(new DateTime(2024, 1, 15), usuario.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20), usuario.AlteradoEm);
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Criar_Usuario_Com_Construtor_Parametros()
        {
            var legadoId = Guid.NewGuid();
            var dataAntes = DateTime.Now;

            var usuario = new Usuario(legadoId, "maria.santos", "Maria Santos");

            Assert.Equal(legadoId, usuario.LegadoId);
            Assert.Equal("maria.santos", usuario.Login);
            Assert.Equal("Maria Santos", usuario.Nome);
            Assert.True(usuario.CriadoEm >= dataAntes);
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Definir_Status_Ativo_Ao_Criar_Usuario()
        {
            var legadoId = Guid.NewGuid();

            var usuario = new Usuario(legadoId, "teste.usuario", "Teste Usuario");

            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Ao_Criar_Usuario()
        {
            var legadoId = Guid.NewGuid();
            var dataAntes = DateTime.Now;

            var usuario = new Usuario(legadoId, "teste.usuario", "Teste Usuario");
            var dataDepois = DateTime.Now;

            Assert.True(usuario.CriadoEm >= dataAntes && usuario.CriadoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Alterar_Usuario()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");
            var dataAntes = DateTime.Now;

            usuario.Alterar("joao.silva.novo", "João Silva Novo");
            var dataDepois = DateTime.Now;

            Assert.Equal("joao.silva.novo", usuario.Login);
            Assert.Equal("João Silva Novo", usuario.Nome);
            Assert.NotNull(usuario.AlteradoEm);
            Assert.True(usuario.AlteradoEm >= dataAntes && usuario.AlteradoEm <= dataDepois);
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Manter_Status_Ativo_Ao_Alterar_Usuario()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");

            usuario.Alterar("joao.silva.novo", "João Silva Novo");

            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Ao_Alterar_Usuario()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");
            var dataAntes = DateTime.Now;

            usuario.Alterar("joao.silva.novo", "João Silva Novo");
            var dataDepois = DateTime.Now;

            Assert.NotNull(usuario.AlteradoEm);
            Assert.True(usuario.AlteradoEm >= dataAntes && usuario.AlteradoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Inativar_Usuario()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");
            var dataAntes = DateTime.Now;

            usuario.Inativar();
            var dataDepois = DateTime.Now;

            Assert.Equal(StatusGeral.Inativo, usuario.Status);
            Assert.NotNull(usuario.AlteradoEm);
            Assert.True(usuario.AlteradoEm >= dataAntes && usuario.AlteradoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Ao_Inativar_Usuario()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");
            var dataAntes = DateTime.Now;

            usuario.Inativar();
            var dataDepois = DateTime.Now;

            Assert.NotNull(usuario.AlteradoEm);
            Assert.True(usuario.AlteradoEm >= dataAntes && usuario.AlteradoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Aceitar_Login_Vazio()
        {
            var legadoId = Guid.NewGuid();

            var usuario = new Usuario(legadoId, "", "Teste Usuario");

            Assert.Equal("", usuario.Login);
        }

        [Fact]
        public void Deve_Aceitar_Nome_Vazio()
        {
            var legadoId = Guid.NewGuid();

            var usuario = new Usuario(legadoId, "teste.usuario", "");

            Assert.Equal("", usuario.Nome);
        }

        [Fact]
        public void Deve_Aceitar_Login_Longo()
        {
            var legadoId = Guid.NewGuid();
            var loginLongo = new string('a', 500);

            var usuario = new Usuario(legadoId, loginLongo, "Teste Usuario");

            Assert.Equal(loginLongo, usuario.Login);
            Assert.Equal(500, usuario.Login.Length);
        }

        [Fact]
        public void Deve_Aceitar_Nome_Longo()
        {
            var legadoId = Guid.NewGuid();
            var nomeLongo = new string('A', 500);

            var usuario = new Usuario(legadoId, "teste.usuario", nomeLongo);

            Assert.Equal(nomeLongo, usuario.Nome);
            Assert.Equal(500, usuario.Nome.Length);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Empty()
        {
            var legadoIdEmpty = Guid.Empty;

            var usuario = new Usuario(legadoIdEmpty, "teste.usuario", "Teste Usuario");

            Assert.Equal(Guid.Empty, usuario.LegadoId);
        }

        [Fact]
        public void Deve_Alterar_Com_Login_Vazio()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");

            usuario.Alterar("", "João Silva Novo");

            Assert.Equal("", usuario.Login);
            Assert.Equal("João Silva Novo", usuario.Nome);
        }

        [Fact]
        public void Deve_Alterar_Com_Nome_Vazio()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");

            usuario.Alterar("joao.silva.novo", "");

            Assert.Equal("joao.silva.novo", usuario.Login);
            Assert.Equal("", usuario.Nome);
        }

        [Fact]
        public void Deve_Manter_LegadoId_Apos_Alterar()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");

            usuario.Alterar("joao.silva.novo", "João Silva Novo");

            Assert.Equal(legadoId, usuario.LegadoId);
        }

        [Fact]
        public void Deve_Manter_LegadoId_Apos_Inativar()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");

            usuario.Inativar();

            Assert.Equal(legadoId, usuario.LegadoId);
        }

        [Fact]
        public void Deve_Manter_Login_E_Nome_Apos_Inativar()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");

            usuario.Inativar();

            Assert.Equal("joao.silva", usuario.Login);
            Assert.Equal("João Silva", usuario.Nome);
        }

        [Fact]
        public void Deve_Permitir_Alterar_Usuario_Inativo()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");
            usuario.Inativar();

            usuario.Alterar("joao.silva.novo", "João Silva Novo");

            Assert.Equal("joao.silva.novo", usuario.Login);
            Assert.Equal("João Silva Novo", usuario.Nome);
            Assert.Equal(StatusGeral.Ativo, usuario.Status);
        }

        [Fact]
        public void Deve_Permitir_Inativar_Usuario_Ja_Inativo()
        {
            var legadoId = Guid.NewGuid();
            var usuario = new Usuario(legadoId, "joao.silva", "João Silva");
            usuario.Inativar();

            usuario.Inativar();

            Assert.Equal(StatusGeral.Inativo, usuario.Status);
        }
    }
}