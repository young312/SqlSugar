using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Express
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Express
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/23 14:23:02
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Express
{
    public class PropertyExpress
    {
        /// <summary>
        /// 获取属性的名称和值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static IDictionary<String, Object> GetPropertiesValue<T>(T Param) where T : class, new()
        {
            ParameterExpression Parameter = Expression.Parameter(Param.GetType(), "t");
            Dictionary<String, Object> Map = new Dictionary<String, Object>();
            Param.GetType().GetProperties().ToList().ForEach(item =>
            {
                MemberExpression PropertyExpress = Expression.Property(Parameter, item);
                UnaryExpression ConvterExpress = Expression.Convert(PropertyExpress, typeof(object));
                Func<T, Object> GetValueFunc = Expression.Lambda<Func<T, object>>(ConvterExpress, Parameter).Compile();
                Map.Add(item.Name, GetValueFunc(Param));
            });
            return Map;
        }
        /// <summary>
        /// 指定属性赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public static Action<T, Object> SetProptertyValue<T>(String PropertyName) where T : class, new()
        {
            var type = typeof(T);
            var property = type.GetProperty(PropertyName);

            var objectParameterExpression = Expression.Parameter(typeof(object), "obj");
            var objectUnaryExpression = Expression.Convert(objectParameterExpression, type);

            var valueParameterExpression = Expression.Parameter(typeof(object), "val");
            var valueUnaryExpression = Expression.Convert(valueParameterExpression, property.PropertyType);

            // 调用给属性赋值的方法
            var body = Expression.Call(objectUnaryExpression, property.GetSetMethod(), valueUnaryExpression);
            var expression = Expression.Lambda<Action<T, object>>(body, objectParameterExpression, valueParameterExpression);

            return expression.Compile();
        }
        /// <summary>
        /// 获取多字段表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="AnonmouseType"></param>
        /// <returns></returns>
        public static Expression<Func<T, Object>> GetExpression<T>(T Entity,Type AnonmouseType)
        {
            List<Expression> Exps = new List<Expression>();
            ParameterExpression Parameter = Expression.Parameter(typeof(T), "t");
            var Constructor = AnonmouseType.GetType().GetConstructors().FirstOrDefault();
            typeof(T).GetProperties().ToList().ForEach(x =>
            {
                MemberExpression PropertyExpress = Expression.Property(Parameter, x.Name);
                UnaryExpression ConvterExpress = Expression.Convert(PropertyExpress, typeof(object));
                Exps.Add(ConvterExpress);
            });
            return Expression.Lambda<Func<T, object>>(Expression.New(Constructor, Exps), Parameter);
        }
    }
}
