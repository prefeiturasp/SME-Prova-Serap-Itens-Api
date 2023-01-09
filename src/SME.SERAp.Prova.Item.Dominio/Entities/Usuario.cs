using SME.SERAp.Prova.Item.Dominio.Entities;
using System;

namespace SME.SERAp.Prova.Item.Dominio
{
    public class Usuario : EntidadeBase
    {
        public Usuario() { }
        public Usuario(Guid legadoId, string login, string nome)
        {
            LegadoId = legadoId;
            Login = login;
            Nome = nome;
            CriadoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Alterar(string login, string nome)
        {
            Login = login;
            Nome = nome;
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Ativo;
        }

        public void Inativar()
        {
            AlteradoEm = DateTime.Now;
            Status = StatusGeral.Inativo;
        }

        public Guid LegadoId { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
        public StatusGeral Status { get; set; }
    }
}
