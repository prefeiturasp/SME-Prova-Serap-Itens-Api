using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Linq;

namespace SME.SERAp.Prova.Item.Api.Filters
{
    public class ValidaDtoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new FalhaValidacaoResult(context.ModelState);
            }
        }

        public class FalhaValidacaoResult : ObjectResult
        {
            public FalhaValidacaoResult(ModelStateDictionary modelState)
                : base(RetornaBaseModel(modelState))
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity;
            }

            public static RetornoBaseDto RetornaBaseModel(ModelStateDictionary modelState)
            {
                var dto = new RetornoBaseDto();
                dto.Mensagens = modelState.Keys
                       .SelectMany(key => modelState[key].Errors.Select(x => new string(x.ErrorMessage)))
                       .ToList();
                return dto;
            }
        }
    }
}
