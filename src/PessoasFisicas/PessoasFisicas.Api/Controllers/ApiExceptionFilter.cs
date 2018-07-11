using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Common;

namespace PessoasFisicas.Api.Controllers
{
	public class ApiExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			if (context.Exception != null)
			{

				var statusCode = context.Exception is ApplicationException ? 
								 StatusCodes.Status400BadRequest : 
								 StatusCodes.Status500InternalServerError;

				var objectResult = new ObjectResult(new
				{
					StatusCode = statusCode,
					Value = context.Exception.Message
				});

				context.Result = objectResult;

			}
		}
	}
}
