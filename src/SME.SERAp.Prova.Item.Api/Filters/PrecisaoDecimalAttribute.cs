using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Api.Filters
{
    public class PrecisaoDecimalAttribute : RegularExpressionAttribute
    {

        public PrecisaoDecimalAttribute(int precision, int scale) : base($@"^(0|-?\d{{0,{precision - scale}}}(\.\d{{0,{scale}}})?)$")
        { }

    }
}