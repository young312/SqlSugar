using Edna.Extension.Attributes.RoleHandler;
using Edna.Extension.Caches;
using Edna.Extension.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Attributes.PermissionHandler
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Attributes.PermissionHandler
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/22 9:51:33
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Attributes.PermissionHandler
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                if (context.User.IsInRole("Admin"))
                    context.Succeed(requirement);
                else
                {
                    var UserIdClaim = context.User.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
                    if (UserIdClaim != null)
                    {
                        AdminRoleViewModel AdminRole = await CacheFacoty.GetCache<AdminRoleViewModel>(typeof(AdminRoleViewModel).FullName);
                        if (AdminRole.RolePermissionId == Guid.Parse(UserIdClaim.Value))
                            if (AdminRole.HandlerRole.StartsWith(requirement.Name))
                                context.Succeed(requirement);
                    }
                }
            }
        }
    }
}
