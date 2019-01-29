using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.SessionExtension
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.SessionExtension
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/21 16:01:41
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.SessionExtension
{
    public static class SessionMap
    {
        /// <summary>
        ///添加Session
        /// </summary>
        /// <param name="value"></param>
        public static void SetSession<T>(this ISession session, String key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 取出Session
        /// </summary>
        /// <returns></returns>
        public static T GetSession<T>(this ISession session, String key)
        {
            return session.GetString(key) == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="Key"></param>
        public static void DeleteSession(this ISession session, String Key)
        {
            session.Remove(Key);
        }
        /// <summary>
        /// 清空Session
        /// </summary>
        /// <param name="session"></param>
        public static void ClearSession(this ISession session)
        {
            session.Clear();
        }
    }
}
