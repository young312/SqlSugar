using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Configuration.ModelEnum
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Configuration.ModelEnum
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/22 14:38:50
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Configuration.ModelEnum
{
    /// <summary>
    /// 审核枚举
    /// </summary>
    public enum AuditStatusEnum
    {
        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        AccessFail,
        /// <summary>
        /// 等待审核
        /// </summary>
        [Description("等待审核")]
        WaitAudti,
        /// <summary>
        /// 待上级审核
        /// </summary>
        [Description("待上级审核")]
        WaitSuperiorAudit,
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        AccessSuccess,
        /// <summary>
        /// 等待终审
        /// </summary>
        [Description("等待终审")]
        WaitFinal,
        /// <summary>
        /// 终审不通过
        /// </summary>
        [Description("终审不通过")]
        AccessFinalFail,
        /// <summary>
        /// 终审通过
        /// </summary>
        [Description("终审通过")]
        AccessFinalSuccess

    }
}
