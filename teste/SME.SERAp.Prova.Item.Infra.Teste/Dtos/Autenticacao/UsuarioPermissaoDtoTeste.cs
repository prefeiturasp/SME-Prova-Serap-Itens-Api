using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Autenticacao
{
    public class UsuarioPermissaoDtoTeste
    {
        [Fact]
        public void Deve_Criar_Instancia_Com_Construtor_Vazio()
        {
            var dto = new UsuarioPermissaoDto();

            Assert.NotNull(dto);
            Assert.Null(dto.Login);
            Assert.Null(dto.Nome);
            Assert.Null(dto.Grupo);
            Assert.False(dto.PermiteConsultar);
            Assert.False(dto.PermiteInserir);
            Assert.False(dto.PermiteAlterar);
            Assert.False(dto.PermiteExcluir);
        }

        [Fact]
        public void Deve_Criar_Instancia_Com_Construtor_Parametrizado()
        {
            var login = "usuario.teste";
            var nome = "Usuário de Teste";
            var grupo = "Administrador";
            var permiteConsultar = true;
            var permiteInserir = true;
            var permiteAlterar = true;
            var permiteExcluir = true;

            var dto = new UsuarioPermissaoDto(login, nome, grupo, permiteConsultar, permiteInserir, permiteAlterar, permiteExcluir);

            Assert.Equal(login, dto.Login);
            Assert.Equal(nome, dto.Nome);
            Assert.Equal(grupo, dto.Grupo);
            Assert.True(dto.PermiteConsultar);
            Assert.True(dto.PermiteInserir);
            Assert.True(dto.PermiteAlterar);
            Assert.True(dto.PermiteExcluir);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var login = "maria.santos";
            var nome = "Maria Santos";
            var grupo = "Coordenador";
            var permiteConsultar = true;
            var permiteInserir = false;
            var permiteAlterar = true;
            var permiteExcluir = false;

            var dto = new UsuarioPermissaoDto
            {
                Login = login,
                Nome = nome,
                Grupo = grupo,
                PermiteConsultar = permiteConsultar,
                PermiteInserir = permiteInserir,
                PermiteAlterar = permiteAlterar,
                PermiteExcluir = permiteExcluir
            };

            Assert.Equal(login, dto.Login);
            Assert.Equal(nome, dto.Nome);
            Assert.Equal(grupo, dto.Grupo);
            Assert.True(dto.PermiteConsultar);
            Assert.False(dto.PermiteInserir);
            Assert.True(dto.PermiteAlterar);
            Assert.False(dto.PermiteExcluir);
        }

        [Fact]
        public void Deve_Aceitar_Todas_Permissoes_Como_True()
        {
            var dto = new UsuarioPermissaoDto("admin", "Administrador", "Admin", true, true, true, true);

            Assert.True(dto.PermiteConsultar);
            Assert.True(dto.PermiteInserir);
            Assert.True(dto.PermiteAlterar);
            Assert.True(dto.PermiteExcluir);
        }

        [Fact]
        public void Deve_Aceitar_Todas_Permissoes_Como_False()
        {
            var dto = new UsuarioPermissaoDto("visitante", "Visitante", "Convidado", false, false, false, false);

            Assert.False(dto.PermiteConsultar);
            Assert.False(dto.PermiteInserir);
            Assert.False(dto.PermiteAlterar);
            Assert.False(dto.PermiteExcluir);
        }

        [Fact]
        public void Deve_Aceitar_Login_Vazio()
        {
            var dto = new UsuarioPermissaoDto(string.Empty, "Nome Teste", "Grupo", true, false, false, false);

            Assert.Equal(string.Empty, dto.Login);
        }

        [Fact]
        public void Deve_Aceitar_Login_Nulo()
        {
            var dto = new UsuarioPermissaoDto(null, "Nome Teste", "Grupo", true, false, false, false);

            Assert.Null(dto.Login);
        }

        [Fact]
        public void Deve_Aceitar_Nome_Vazio()
        {
            var dto = new UsuarioPermissaoDto("login.teste", string.Empty, "Grupo", true, false, false, false);

            Assert.Equal(string.Empty, dto.Nome);
        }

        [Fact]
        public void Deve_Aceitar_Nome_Nulo()
        {
            var dto = new UsuarioPermissaoDto("login.teste", null, "Grupo", true, false, false, false);

            Assert.Null(dto.Nome);
        }

        [Fact]
        public void Deve_Aceitar_Grupo_Vazio()
        {
            var dto = new UsuarioPermissaoDto("login.teste", "Nome Teste", string.Empty, true, false, false, false);

            Assert.Equal(string.Empty, dto.Grupo);
        }

        [Fact]
        public void Deve_Aceitar_Grupo_Nulo()
        {
            var dto = new UsuarioPermissaoDto("login.teste", "Nome Teste", null, true, false, false, false);

            Assert.Null(dto.Grupo);
        }

        [Fact]
        public void Deve_Permitir_Modificar_Propriedades_Apos_Criacao()
        {
            var dto = new UsuarioPermissaoDto("usuario1", "Usuario Um", "Grupo1", false, false, false, false);

            dto.Login = "usuario2";
            dto.Nome = "Usuario Dois";
            dto.Grupo = "Grupo2";
            dto.PermiteConsultar = true;
            dto.PermiteInserir = true;
            dto.PermiteAlterar = true;
            dto.PermiteExcluir = true;

            Assert.Equal("usuario2", dto.Login);
            Assert.Equal("Usuario Dois", dto.Nome);
            Assert.Equal("Grupo2", dto.Grupo);
            Assert.True(dto.PermiteConsultar);
            Assert.True(dto.PermiteInserir);
            Assert.True(dto.PermiteAlterar);
            Assert.True(dto.PermiteExcluir);
        }

        [Fact]
        public void Deve_Aceitar_Permissoes_Mistas()
        {
            var dto = new UsuarioPermissaoDto("editor", "Editor", "Editores", true, true, false, false);

            Assert.True(dto.PermiteConsultar);
            Assert.True(dto.PermiteInserir);
            Assert.False(dto.PermiteAlterar);
            Assert.False(dto.PermiteExcluir);
        }
    }
}