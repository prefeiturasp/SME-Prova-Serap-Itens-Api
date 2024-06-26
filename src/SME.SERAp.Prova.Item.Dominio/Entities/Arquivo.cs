﻿using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Arquivo : EntidadeBase
    {
        public Arquivo() { }

        public Arquivo(long legadoId, string nome, string caminho,
            string contentType, int situacao, DateTime criadoEm) : this()
        {
            if (Id == 0)
                AlteradoEm = null;

            LegadoId = legadoId;
            Nome = nome;
            Caminho = caminho;
            ContentType = contentType;
            Situacao = situacao;
            CriadoEm = criadoEm;
        }

        public long LegadoId { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string ContentType { get; set; }
        public int Situacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
    }
}
