using Edna.Extension.Infrastructure.GeneralMiddleWare;
using Edna.Extension.LoggerFactory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Edna.Extension.FilterGroup
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/29 14:23:21
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************/
#endregion
namespace Edna.Extension.Filters
{
    public class ActionFilter : IActionFilter
    {
        /// <summary>
        /// 第四执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                string Path = context.Exception.Source;
                string WebPath = context.HttpContext.Request.Path;
                string MethodName = context.Exception.TargetSite.Name;
                string Parameter = string.Empty;
                string Message = context.Exception.Message;
                context.Exception.TargetSite.GetParameters().ToList().ForEach(t =>
                {
                    Parameter += "[" + t.Name + "]";
                });
                LogFactoryExtension.WriteError(Path, MethodName, Parameter, Message, WebPath);
                return;
            }
            ResultApiMiddleWare Result = ResultApiMiddleWare.Instance(true, context.HttpContext.Response.StatusCode, (context.Result as ObjectResult).Value, "执行成功!");
            context.Result = new ObjectResult(Result);
        }
        /// <summary>
        /// 第三执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
