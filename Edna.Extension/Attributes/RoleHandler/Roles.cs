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
* 创建时间 ：2019/1/22 9:41:38
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Attributes.RoleHandler
{
    public static class Roles
    {
        public const String Admin = "Admin";
        public const String AdminCreate = "Admin.Create";
        public const String AdminRead = "Admin.Read";
        public const String AdminUpdate = "Admin.Update";
        public const String AdminDelete = "Admin.Delete";

        public const String User = "User";
        public const String UserCreate = "User.Create";
        public const String UserRead = "User.Read";
        public const String UserUpdate = "User.Update";
        public const String UserDelete = "User.Delete";
    }
}
