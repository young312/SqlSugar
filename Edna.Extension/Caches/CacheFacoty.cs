using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Caches
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Caches
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/18 16:55:38
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Caches
{
    public class CacheFacoty
    {
        public static RedisCache Redis=>new RedisCache();
        /// <summary>
        /// 缓存中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static async Task<T> GetCache<T>(string CacheKey)
        {
            return await RedisCache.StringGetAsync<T>(CacheKey);
        }
        /// <summary>
        /// 删除某个缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        public static async Task RemoveCache(string CacheKey)
        {
            await RedisCache.KeyDeleteAsync(CacheKey);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public static async Task RemoveCache()
        {
            await RedisCache.DeleteRedisDataBaseAsync();
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        /// <param name="hours"></param>
        public static async Task WriteCache<T>(T obj, string CacheKey, int hours)
        {
            await RedisCache.StringSetAsync<T>(CacheKey, obj, (DateTime.Now.AddHours(hours) - DateTime.Now));
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="CacheKey"></param>
        public static async Task WriteCache<T>(T obj, string CacheKey)
        {
            await RedisCache.StringSetAsync<T>(CacheKey, obj);
        }
    }
}
