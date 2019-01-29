using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Quartz
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Quartz
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/23 10:56:45
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Quartz
{
    /// <summary>
    /// 作业实体
    /// </summary>
    public class QuartzMap
    {
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 执行任务间隔时间单位秒
        /// </summary>
        public int IntervalSecond { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public int RunTimes { get; set; }
        /// <summary>
        /// 时间表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string JobDetail { get; set; }
    }
}
