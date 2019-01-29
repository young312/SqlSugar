using Edna.Configuration;
using Edna.EntityCore.Caches;
using Edna.EntityCore.Model;
using Edna.Extension.LoggerFactory;
using SqlSugar;
using System;
using System.Linq;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Db.EntityCore
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/16 17:04:41
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************/
#endregion
namespace Edna.EntityCore
{
    /// <summary>
    /// 获取SQLDB上下文
    /// </summary>
    public class SugerDbContext
    {
        /// <summary>
        /// 获取连接客服端
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient Emily
        {
            get
            {
                SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = BaseConfig.ConnectionString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute,
                });
                Db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices
                {
                    DataInfoCacheService = new DbCache()
                };
                Db.QueryFilter.Add(new SqlFilterItem()
                {
                    FilterValue = t =>
                    {
                        return new SqlFilterResult() { Sql = " IsDelete=0" };
                    },
                    IsJoinQuery = false
                });
                Db.Aop.OnError = (Ex) =>
                {
                    var Logs = $"SQL语句：{Ex.Sql}[SQL参数：{Ex.Parametres}]";
                    //启用NLog
                    LogFactoryExtension.WriteSqlError(Logs);
                };
                Db.Aop.OnLogExecuted = (Sql, Args) =>
                {
                    var Logs = $"SQL语句：{Sql}[SQL参数：{Db.Utilities.SerializeObject(Args.ToDictionary(t => t.ParameterName, t => t.Value))}][SQL执行时间：{Db.Ado.SqlExecutionTime.TotalMilliseconds}毫秒]";
                    Db.MappingTables.ToList().ForEach(t =>
                    {
                        if (Sql.Contains($"[{t.DbTableName}]")&&!Sql.Contains("ALTER TABLE"))
                            //启用NLog
                            LogFactoryExtension.WriteSqlWarn(Logs);
                    });
                };
                Type[] ModelTypes = typeof(SugerDbContext).GetTypeInfo().Assembly.GetTypes().Where(t => t.BaseType == typeof(BaseModel)).ToArray();
                Db.CodeFirst.InitTables(ModelTypes);
                return Db;
            }
        }
    }
}
