using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.EntityCore
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.EntityCore
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/2/13 14:49:31
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.EntityCore
{
    public enum DbReturnTypes
    {
        #region EnumOfInsert
        /// <summary>
        /// Return Boolean
        /// </summary>
        Default = 0,
        /// <summary>
        ///  Return Rows
        /// </summary>
        Rowspan = 1,
        /// <summary>
        ///  Return Int
        /// </summary>
        Integer = 2,
        /// <summary>
        /// Return Long
        /// </summary>
        BigInteger = 3,
        /// <summary>
        /// Return Entity
        /// </summary>
        Model = 4
        #endregion
    }
}
