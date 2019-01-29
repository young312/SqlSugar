using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.EntityCore.Model.SystemModel
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.EntityCore.Model.SystemModel
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/22 14:17:37
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.EntityCore.Model.SystemModel
{
    /// <summary>
    /// 权限许可表
    /// </summary>
    [SugarTable("System_RolePermission", "权限许可表")]
    public class RolePermission:BaseModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "NVARCHAR", ColumnDescription = "角色名称", Length = 50)]
        public string RoleName { get; set; }
        /// <summary>
        /// 操作角色
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "NVARCHAR", ColumnDescription = "操作角色", Length = 50)]
        public string HandlerRole { get; set; }
    }
}
