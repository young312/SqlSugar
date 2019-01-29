using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Edna.EntityCore.Model.SystemModel
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/27 15:01:21
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************/
#endregion
namespace Edna.EntityCore.Model.SystemModel
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    [SugarTable("System_Administrator","系统用户表")]
    public class Administrator:BaseModel
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "NVARCHAR", ColumnDescription = "管理员",Length =50)]
        public string AdminName { get; set; }
        /// <summary>
        /// 账号
        /// </summary> 
        [SugarColumn(IsNullable = true, ColumnDataType = "NVARCHAR", ColumnDescription = "账号", Length = 50)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "NVARCHAR", ColumnDescription = "密码", Length = 50)]
        public string PassWord { get; set; }
        /// <summary>
        /// 权限许可ID
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "权限许可ID")]
        public Guid RolePermissionId { get; set; }
    }
}
