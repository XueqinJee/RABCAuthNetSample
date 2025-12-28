using AcAuthNetSample.Core.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RbacNetSample.Web.Configuration.Filters {
    public class ApiExceptionFilter : IExceptionFilter {

        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            // 处理异常
            context.ExceptionHandled = true;
            context.Result = JsonResultHelper.ServerError(msg: context.Exception.Message);
        }
    }
}
