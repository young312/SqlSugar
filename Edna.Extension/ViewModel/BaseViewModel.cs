using Edna.Configuration.ModelEnum;
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
* 创建时间 ：2019/1/22 14:37:24
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.ViewModel
{
    public class BaseViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid PrimaryId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public string DeleteUser { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid? CreateUserId { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        public Guid? UpdateUserId { get; set; }
        /// <summary>
        /// 删除人Id
        /// </summary>
        public Guid? DeleteUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatusEnum? AuditStatus { get; set; }
    }
}
