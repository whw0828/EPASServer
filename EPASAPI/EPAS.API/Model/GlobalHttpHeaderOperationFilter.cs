using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EPAS.API.Model
{
    public class GlobalHttpHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            var actionAttrs = context.ApiDescription.ActionAttributes();

            var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));
            //该特性用于标记在授权期间要跳过 
            //whw 2019.5.13 去掉单点登录
            isAllowAnonymous = true;
            if (!isAllowAnonymous)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "EPAS-Token",
                    In = "header",
                    Type = "string",
                    Description = "当前登录用户登录token",
                    Required = false
                });
            }
        }
    }
}
