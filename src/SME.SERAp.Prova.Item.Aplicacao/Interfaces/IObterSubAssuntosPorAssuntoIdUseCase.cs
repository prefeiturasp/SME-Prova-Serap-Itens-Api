﻿using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterSubAssuntosPorAssuntoIdUseCase
    {
        Task<IEnumerable<SelectDto>> Executar(long assuntoId);
    }
}
