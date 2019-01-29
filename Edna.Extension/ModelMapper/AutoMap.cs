using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Edna.Extension.ModelMapper
* 项目描述 ：
* 类 描 述 ：
* 命名空间 ：Edna.Extension.ModelMapper
* 机器名称 ：EMILY 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2019/1/21 11:09:21
*******************************************************************
* Copyright @ Emily 2019. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
namespace Edna.Extension.ModelMapper
{
    public static class AutoMap
    {
        public static  T AutoMapper<T>(this Object obj)
        {
            if (obj == null) return default(T);
            IMapper mapper = new MapperConfiguration(t => t.CreateMap(obj.GetType(), typeof(T))).CreateMapper();
            return mapper.Map<T>(obj);
        }
        public static T AutoMapper<T>(this Object obj, String IgnoreNames)
        {
            if (obj == null) return default(T);
            MapperConfigurationExpression expression = new MapperConfigurationExpression();
            IMappingExpression mapping = expression.CreateMap(obj.GetType(), typeof(T));
            if (IgnoreNames.Contains("|"))
                IgnoreNames.Split('|').ToList().ForEach(t =>{mapping.ForMember(t, x => x.Ignore());});
            else
                mapping.ForMember(IgnoreNames, x => x.Ignore());
            IMapper mapper = new MapperConfiguration(expression).CreateMapper();
            return mapper.Map<T>(obj);
        }
    }
}
