using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirTesteCommand : IRequest<long>
    {
        public InserirTesteCommand(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; set; }
    }
}
