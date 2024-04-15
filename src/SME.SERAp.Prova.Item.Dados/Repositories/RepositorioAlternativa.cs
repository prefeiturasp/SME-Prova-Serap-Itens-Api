using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioAlternativa : RepositorioBase<Alternativa>, IRepositorioAlternativa
    {
        public RepositorioAlternativa(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }
    }
}
