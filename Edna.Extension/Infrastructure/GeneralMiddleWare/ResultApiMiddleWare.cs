using System;
using System.Collections.Generic;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.Infrastructure.GeneralMiddleWare
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.Infrastructure.GeneralMiddleWare
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/17 15:17:38
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.Infrastructure.GeneralMiddleWare
{
    public class ResultApiMiddleWare
    {
        public Boolean IsSuccess { get; set; }
        public Int32 StatusCode { get; set; }
        public Object Data { get; set; }
        public String Info { get; set; }
        public static ResultApiMiddleWare Instance(Object Param)
        {
            return new ResultApiMiddleWare() { Data = Param };
        }
        public static ResultApiMiddleWare Instance(Boolean Flag, Int32 Code, Object Param, String Msg)
        {
            return new ResultApiMiddleWare() { IsSuccess = Flag, StatusCode = Code, Data = Param, Info = Msg };
        }
    }
}
