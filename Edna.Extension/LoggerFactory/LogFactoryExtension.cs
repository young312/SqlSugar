using NLog;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Logger
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Logger
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/17 17:24:14
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.LoggerFactory
{
    /// <summary>
    /// NLog日志工厂
    /// </summary>
    public static class LogFactoryExtension
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="Msg"></param>
        public static void WriteInfo(string Path, string MethodName, string Parameter, string Msg, string WebPath)
        {
            logger.Info($"异常位置：{Path}，调用方法名：{MethodName}，相关参数：{Parameter}，异常信息：{Msg}，请求路径：{WebPath}");
        }
        /// <summary>
        /// 栈日志
        /// </summary>
        /// <param name="Msg"></param>
        public static void WriteTrace(string Path, string MethodName, string Parameter, string Msg, string WebPath)
        {
            logger.Trace($"异常位置：{Path}，调用方法名：{MethodName}，相关参数：{Parameter}，异常信息：{Msg}，请求路径：{WebPath}");
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="Msg"></param>
        public static void WriteDebug(string Path, string MethodName, string Parameter, string Msg, string WebPath)
        {
            logger.Debug($"异常位置：{Path}，调用方法名：{MethodName}，相关参数：{Parameter}，异常信息：{Msg}，请求路径：{WebPath}");
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="Msg"></param>
        public static void WriteError(string Path, string MethodName, string Parameter, string Msg,string WebPath)
        {
            logger.Error($"异常位置：{Path}，调用方法名：{MethodName}，相关参数：{Parameter}，异常信息：{Msg}，请求路径：{WebPath}");
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="Msg"></param>
        public static void WriteWarn(string Path, string MethodName, string Parameter, string Msg, string WebPath)
        {
            logger.Warn($"异常位置：{Path}，调用方法名：{MethodName}，相关参数：{Parameter}，异常信息：{Msg}，请求路径：{WebPath}");
        }
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="Msg"></param>
        public static void WriteFatal(string Path, string MethodName, string Parameter, string Msg, string WebPath)
        {
            logger.Fatal($"异常位置：{Path}，调用方法名：{MethodName}，相关参数：{Parameter}，异常信息：{Msg}，请求路径：{WebPath}");
        }
        /// <summary>
        /// SQL错误记录
        /// </summary>
        /// <param name="SQLException"></param>
        public static void WriteSqlError(string SQLException)
        {
            logger.Error(SQLException);
        }
        /// <summary>
        /// SQL执行记录
        /// </summary>
        /// <param name="SQLException"></param>
        public static void WriteSqlWarn(string SQLInfo)
        {
            logger.Warn(SQLInfo);
        }
    }
}
