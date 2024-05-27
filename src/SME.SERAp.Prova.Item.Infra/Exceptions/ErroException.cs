using System;
using Microsoft.AspNetCore.Http;

namespace SME.SERAp.Prova.Item.Infra.Exceptions
{
    public class ErroException : Exception
    {
        public ErroException(string message) : base(message)
        {
        }
        
        public int StatusCode => StatusCodes.Status500InternalServerError;
    }
}