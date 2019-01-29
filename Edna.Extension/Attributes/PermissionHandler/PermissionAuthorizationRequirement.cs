using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Attributes.RoleHandler
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Attributes.RoleHandler
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/22 9:49:00
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Attributes.PermissionHandler
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Name { get; set; }
        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }
    }
}
