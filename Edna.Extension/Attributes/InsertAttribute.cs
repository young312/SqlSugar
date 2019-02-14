using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Attributes
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Attributes
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/2/14 9:23:57
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class InsertAttribute : Attribute
    {
        public InsertAttribute() { }
    }
}
