using Edna.Configuration;
using Edna.EntityCore.Caches;
using Edna.EntityCore.Model;
using Edna.Extension.Attributes;
using Edna.Extension.LoggerFactory;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
                //Db.QueryFilter.Add(new SqlFilterItem()
                //{
                //    FilterValue = t =>
                //    {
                //        return new SqlFilterResult() { Sql = " IsDelete=0" };
                //    },
                //    IsJoinQuery = false
                //}).Add(new SqlFilterItem()
                //{
                //    FilterName = "SoftDeletion",
                //    FilterValue = t =>
                //    {
                //        return new SqlFilterResult { Sql = " IsDelete=1" };
                //    },
                //    IsJoinQuery = false
                //});
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
                        if (Sql.Contains($"[{t.DbTableName}]") && !Sql.Contains("ALTER TABLE"))
                            //启用NLog
                            LogFactoryExtension.WriteSqlWarn(Logs);
                    });
                };
                Type[] ModelTypes = typeof(SugerDbContext).GetTypeInfo().Assembly.GetTypes().Where(t => t.BaseType == typeof(BaseModel)).ToArray();
                Db.CodeFirst.InitTables(ModelTypes);
                return Db;
            }
        }
        /// <summary>
        /// 新增数据通用
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual async Task<Object> InsertData<Entity>(Entity entity, DbReturnTypes type = DbReturnTypes.Default) where Entity : class, new()
        {
            IInsertable<Entity> Insert = Emily.Insertable<Entity>(entity);
            switch (type)
            {
                case DbReturnTypes.Rowspan:
                    return await Insert.ExecuteCommandAsync();
                case DbReturnTypes.Integer:
                    return await Insert.ExecuteReturnIdentityAsync();
                case DbReturnTypes.BigInteger:
                    return await Insert.ExecuteReturnBigIdentityAsync();
                case DbReturnTypes.Model:
                    return await Insert.ExecuteReturnEntityAsync();
                default:
                    return await Insert.ExecuteCommandIdentityIntoEntityAsync();
            }
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<Object> SoftDeletion<Entity>(List<Entity> entity) where Entity : class, new()
        {
            var Dynamic = new { F1 = (object)null, F2 = (object)null, F3 = (object)null, F4 = (object)null };
            var Constructor = Dynamic.GetType().GetConstructors().FirstOrDefault();
            List<Expression> Exps = new List<Expression>();
            ParameterExpression Parameter = Expression.Parameter(typeof(Entity), "t");
            typeof(Entity).GetProperties().ToList().ForEach(x =>
            {
                if (x.GetCustomAttributes(true).Any(t => t.GetType() == typeof(RemoveAttribute)))
                {
                    MemberExpression PropertyExpress = Expression.Property(Parameter, x.Name);
                    UnaryExpression ConvterExpress = Expression.Convert(PropertyExpress, typeof(object));
                    Exps.Add(ConvterExpress);
                }
            });
            var Exp = Expression.Lambda<Func<Entity, object>>(Expression.New(Constructor, Exps), Parameter);
            await Task.CompletedTask;
            return Emily.Saveable(entity).UpdateColumns(Exp).ExecuteReturnList();
        }
    }

}
