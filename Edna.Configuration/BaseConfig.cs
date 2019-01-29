using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Edna.Configuration
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/19 10:33:55
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************/
#endregion
namespace Edna.Configuration
{
    public class BaseConfig
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static String ConnectionString { get; set; }
        /// <summary>
        /// 所有程序集
        /// </summary>
        public static IList<Assembly> Assembly { get; set; }
        /// <summary>
        /// Redis缓存链接字符串
        /// </summary>
        public static string RedisConnectionString { get; set; }
    }
}
