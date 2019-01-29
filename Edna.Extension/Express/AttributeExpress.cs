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
* 创建时间 ：2019/1/23 13:39:51
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Express
{
    public class AttributeExpress
    {
        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <typeparam name="K">Model</typeparam>
        /// <param name="Express">表达式</param>
        /// <returns></returns>
        public static T GetAttributeType<T, K>(Expression<Func<K, Object>> Express)
        {
            if (Express == null) return default(T);
            MemberExpression Exp = (MemberExpression)Express.Body;
            var Attribute = (T)Exp.Member.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            return Attribute;
        }
    }
}
