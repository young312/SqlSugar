using Edna.Extension.Attributes.PermissionHandler;
using Edna.Extension.AutofacIoc;
using Edna.Extension.Infrastructure.GeneralMiddleWare;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Attributes
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Attributes
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/21 17:14:50
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public String Name { get; set; }
        public AuthorAttribute(string name)
        {
            Name = name;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, new PermissionAuthorizationRequirement(Name));
            if (!authorizationResult.Succeeded)
            {
                context.Result = new ObjectResult(ResultApiMiddleWare.Instance(false, StatusCodes.Status401Unauthorized, null, "无权访问!"));
            }
        }
    }
}
