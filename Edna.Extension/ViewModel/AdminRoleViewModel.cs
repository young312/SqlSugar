using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.ViewModel
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.ViewModel
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/22 14:37:11
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.ViewModel
{
    public class AdminRoleViewModel: BaseViewModel
    {
        /// <summary>
        /// 管理员
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 权限许可ID
        /// </summary>
        public Guid? RolePermissionId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 操作角色
        /// </summary>
        public string HandlerRole { get; set; }
    }
}
