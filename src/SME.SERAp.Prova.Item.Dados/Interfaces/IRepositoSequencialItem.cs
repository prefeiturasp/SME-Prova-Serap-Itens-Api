﻿using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositoSequencialItem : IRepositorioBase<SequencialItem>
    {
        Task<SequencialItem> ObterSequencialItemPorCodigoAreaEDisciplina(long codigoAreaConhecimento, long codigoDisciplina);
    }
}
