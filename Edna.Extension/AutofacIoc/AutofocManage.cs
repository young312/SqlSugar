using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Edna.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Edna.Extension.AutofacIoc
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/28 14:24:49
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************/
#endregion
namespace Edna.Extension.AutofacIoc
{
    /// <summary>
    /// 程序初始化类
    /// </summary>
    public class AutofocManage
    {
        protected ContainerBuilder builder = null;
        protected static readonly IDictionary<Object, Object> AutofacInstance = new Dictionary<Object, Object>();
        protected IContainer Container { get; set; }
        private IList<Assembly> Assembly => BaseConfig.Assembly;
        private IEnumerable<Type> Service => Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IService))));
        public AutofocManage() => builder = new ContainerBuilder();
        /// <summary>
        /// 完成构建
        /// </summary>
        protected void CompleteBuiler() => Container = builder.Build();
        /// <summary>
        /// 取出实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() => Container == null ? default(T) : Container.Resolve<T>();
        /// <summary>
        /// 返回AutoFac服务
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public IServiceProvider ServiceProvider(IServiceCollection Collection)
        {
            //托管.net core自带的DI
            builder.Populate(Collection);
            Register(builder);
            CompleteBuiler();
            return Container.Resolve<IServiceProvider>();
        }
        /// <summary>
        /// 程序注入
        /// </summary>
        /// <param name="builder"></param>
        protected void Register(ContainerBuilder builder)
        {
            //注入请求上下文为了使用PaySharp
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            //业务逻辑注入
            Service.ToList().ForEach(t =>
            {
                if (t.IsClass)
                {
                    builder.RegisterType(Activator.CreateInstance(t).GetType()).As(t.GetInterfaces().Where(x => x.GetInterfaces().Contains(typeof(IService))).FirstOrDefault()).SingleInstance();
                }
            });
        }
        public static AutofocManage CreateInstance(bool CreateNewInstance = false)
        {
            if (CreateNewInstance)
                return new AutofocManage();
            else
            {
                if (AutofacInstance.Count != 0)
                    return (AutofocManage)AutofacInstance[typeof(AutofocManage).Name];
                else
                {
                    AutofocManage autofoc = new AutofocManage();
                    AutofacInstance.Add(typeof(AutofocManage).Name, autofoc);
                    return autofoc;
                }
            }
        }
    }
}
