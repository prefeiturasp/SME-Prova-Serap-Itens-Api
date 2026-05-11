using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio
{
    [Table("usuario")]
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

        [Column("legado_id")]
        public Guid LegadoId { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime? AlteradoEm { get; set; }

        [Column("status")]
        public StatusGeral Status { get; set; }
    }
}