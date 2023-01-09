namespace SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao
{
    public class UsuarioPermissaoDto
    {
        public UsuarioPermissaoDto() { }
        public UsuarioPermissaoDto(string login, string nome, string grupo, bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
        {
            Login = login;
            Nome = nome;
            Grupo = grupo;
            PermiteConsultar = permiteConsultar;
            PermiteInserir = permiteInserir;
            PermiteAlterar = permiteAlterar;
            PermiteExcluir = permiteExcluir;
        }

        public string Login { get; set; }
        public string Nome { get; set; }
        public string Grupo { get; set; }
        public bool PermiteConsultar { get; set; }
        public bool PermiteInserir { get; set; }
        public bool PermiteAlterar { get; set; }
        public bool PermiteExcluir { get; set; }
    }
}
