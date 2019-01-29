using Edna.Extension.Caches;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.EntityCore.Caches
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.EntityCore.Caches
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/18 16:50:02
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.EntityCore.Caches
{
    public class DbCache : RedisCache, ICacheService
    {
        public void Add<T>(string key, T value)
        {
            StringSet(key, value);
        }

        public void Add<T>(string key, T value, int cacheDurationInSeconds)
        {
            StringSet(key, value, (DateTime.Now.AddSeconds(cacheDurationInSeconds) - DateTime.Now));
        }

        public bool ContainsKey<T>(string key)
        {
            return StringGet<T>(key) != null ? true : false;
        }

        public T Get<T>(string key)
        {
            return StringGet<T>(key);
        }

        public IEnumerable<string> GetAllKey<T>()
        {
            return null;
        }

        public T GetOrCreate<T>(string cacheKey, Func<T> create, int cacheDurationInSeconds = int.MaxValue)
        {
            if (this.ContainsKey<T>(cacheKey))
            {
                return this.Get<T>(cacheKey);
            }
            else
            {
                var result = create();
                Add(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }

        public void Remove<T>(string key)
        {
            KeyDelete(key);
        }
    }
}
