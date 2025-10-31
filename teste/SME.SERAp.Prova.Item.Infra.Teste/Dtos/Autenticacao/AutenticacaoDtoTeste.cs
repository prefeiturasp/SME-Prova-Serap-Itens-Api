using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Autenticacao
{
    public class AutenticacaoDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var login = "joao.silva";
            var perfil = "Administrador";

            var dto = new AutenticacaoDto
            {
                Login = login,
                Perfil = perfil
            };

            Assert.Equal(login, dto.Login);
            Assert.Equal(perfil, dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Login_Nulo()
        {
            var dto = new AutenticacaoDto
            {
                Login = null,
                Perfil = "Usuario"
            };

            Assert.Null(dto.Login);
            Assert.Equal("Usuario", dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Perfil_Nulo()
        {
            var dto = new AutenticacaoDto
            {
                Login = "maria.santos",
                Perfil = null
            };

            Assert.Equal("maria.santos", dto.Login);
            Assert.Null(dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Ambos_Nulos()
        {
            var dto = new AutenticacaoDto
            {
                Login = null,
                Perfil = null
            };

            Assert.Null(dto.Login);
            Assert.Null(dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Login_Vazio()
        {
            var dto = new AutenticacaoDto
            {
                Login = string.Empty,
                Perfil = "Gestor"
            };

            Assert.Equal(string.Empty, dto.Login);
            Assert.Equal("Gestor", dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Perfil_Vazio()
        {
            var dto = new AutenticacaoDto
            {
                Login = "carlos.oliveira",
                Perfil = string.Empty
            };

            Assert.Equal("carlos.oliveira", dto.Login);
            Assert.Equal(string.Empty, dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Diferentes_Perfis()
        {
            var dtoAdmin = new AutenticacaoDto { Login = "admin", Perfil = "Administrador" };
            var dtoGestor = new AutenticacaoDto { Login = "gestor1", Perfil = "Gestor" };
            var dtoProfessor = new AutenticacaoDto { Login = "prof1", Perfil = "Professor" };
            var dtoUsuario = new AutenticacaoDto { Login = "user1", Perfil = "Usuario" };

            Assert.Equal("Administrador", dtoAdmin.Perfil);
            Assert.Equal("Gestor", dtoGestor.Perfil);
            Assert.Equal("Professor", dtoProfessor.Perfil);
            Assert.Equal("Usuario", dtoUsuario.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Login_Email()
        {
            var dto = new AutenticacaoDto
            {
                Login = "usuario@exemplo.com.br",
                Perfil = "Professor"
            };

            Assert.Equal("usuario@exemplo.com.br", dto.Login);
            Assert.Equal("Professor", dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Login_CPF()
        {
            var dto = new AutenticacaoDto
            {
                Login = "12345678900",
                Perfil = "Gestor"
            };

            Assert.Equal("12345678900", dto.Login);
            Assert.Equal("Gestor", dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Vazio()
        {
            var dto = new AutenticacaoDto();

            Assert.Null(dto.Login);
            Assert.Null(dto.Perfil);
        }

        [Fact]
        public void Deve_Permitir_Modificacao_Das_Propriedades()
        {
            var dto = new AutenticacaoDto
            {
                Login = "usuario1",
                Perfil = "Usuario"
            };

            Assert.Equal("usuario1", dto.Login);
            Assert.Equal("Usuario", dto.Perfil);

            dto.Login = "usuario2";
            dto.Perfil = "Administrador";

            Assert.Equal("usuario2", dto.Login);
            Assert.Equal("Administrador", dto.Perfil);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Login_Alfanumerico()
        {
            var dto = new AutenticacaoDto
            {
                Login = "user123abc",
                Perfil = "Professor"
            };

            Assert.Equal("user123abc", dto.Login);
        }

        [Fact]
        public void Deve_Criar_AutenticacaoDto_Com_Login_Com_Caracteres_Especiais()
        {
            var dto = new AutenticacaoDto
            {
                Login = "user_123.teste",
                Perfil = "Gestor"
            };

            Assert.Equal("user_123.teste", dto.Login);
        }
    }
}