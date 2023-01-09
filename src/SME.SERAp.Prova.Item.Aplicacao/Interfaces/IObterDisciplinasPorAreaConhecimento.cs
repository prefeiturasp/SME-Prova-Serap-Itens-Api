﻿using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public  interface IObterDisciplinasPorAreaConhecimento
    {
        Task<IEnumerable<Disciplina>> Executar(long areaConhecimentoId);
    }
}
