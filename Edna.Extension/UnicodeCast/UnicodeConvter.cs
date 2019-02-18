using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.UnicodeCast
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.UnicodeCast
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/2/18 15:21:36
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.UnicodeCast
{
    /// <summary>
    /// Unicode和汉字互转
    /// </summary>
    public class UnicodeConvter
    {
        /// <summary>
        /// 汉字转Unicode
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string StringToUnicode(String Source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Unicode转汉字
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string UnicodeToString(String Source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled)
                .Replace(Source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
    }
}
