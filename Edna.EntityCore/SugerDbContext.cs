using Edna.Configuration;
using Edna.EntityCore.Caches;
using Edna.EntityCore.Model;
using Edna.Extension.Attributes;
using Edna.Extension.Express;
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
        /// 更新数据通用
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <param name="Del"></param>
        /// <param name="ObjExp"></param>
        /// <param name="BoolExp"></param>
        /// <returns></returns>
        public virtual async Task<Object> AlterData<Entity>(List<Entity> entity, DbReturnTypes type = DbReturnTypes.AlterSingle, 
            Boolean Del=true, Expression<Func<Entity, Object>> ObjExp = null,Expression < Func<Entity, bool>> BoolExp=null) where Entity : class, new()
        {
            entity.ForEach(t => {
                PropertyExpress.SetProptertyValue<Entity>("UpdateUser")(t, "测试");
                PropertyExpress.SetProptertyValue<Entity>("UpdateUserId")(t, null);
                PropertyExpress.SetProptertyValue<Entity>("UpdateTime")(t, DateTime.Now);
            });
            switch (type)
            {
                case DbReturnTypes.AlterEntity:
                    return await Emily.Updateable(entity).Where(BoolExp).ExecuteCommandAsync();
                case DbReturnTypes.AlterCols:
                    return await Emily.Updateable(entity).UpdateColumns(ObjExp).Where(BoolExp).ExecuteCommandAsync();
                case DbReturnTypes.AlterSoft:
                    entity.ForEach(t => {
                        PropertyExpress.SetProptertyValue<Entity>("IsDelete")(t, Del);
                    });
                    return await Emily.Updateable(entity).UpdateColumns(ObjExp).Where(BoolExp).ExecuteCommandAsync();
                default:
                    return await Emily.Updateable(entity).Where(BoolExp).ExecuteCommandAsync();
            }
        }
        //public virtual async Task<>
    }
}
