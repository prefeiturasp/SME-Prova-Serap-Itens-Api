using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Infra.Attributes
{
    public class PrecisaoDecimalAttribute : RegularExpressionAttribute
    {

        public PrecisaoDecimalAttribute(int precisao, int escala) : base($@"^(0|-?\d{{0,{precisao - escala}}}(\.\d{{0,{escala}}})?)$")
        { }

    }
}