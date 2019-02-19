using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Edna.Configuration;
using Edna.Extension.Attributes.PermissionHandler;
using Edna.Extension.AutofacIoc;
using Edna.Extension.Filters;
using Edna.Extension.Infrastructure.GeneralModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace Edna.ApiCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
            GetJsonFilesConfiger();
            GetAssembly();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AutofocManage.CreateInstance().ServiceProvider(services);
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
                opt.SuppressInferBindingSourcesForParameters = true;
                opt.SuppressConsumesConstraintForFormFileParameters = true;
            });
            //启用权限认证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            //设置数据格式
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            //启用跨域
            services.AddCors(option =>
            {
                option.AddPolicy("EdnaCore", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            });
            //启用Session
            services.AddSession(opt =>
            {
                //Session 5分钟后过期
                opt.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            //启用Swagger
            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new Info { Title = "Api", Version = "v1" });
                //opt.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "Edna.ApiCore.xml"));
            });
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ActionFilter));
                opt.RespectBrowserAcceptHeader = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Nlog
            logger.AddNLog();
            env.ConfigureNLog("Nlog.config");
            //注册权限
            app.UseAuthentication();
            //注册异常中间件
            app.UseMiddleware<ExceptionMiddleWare>();
            //注册跨域
            app.UseCors("EdnaCore");
            //注册Session
            app.UseSession();
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            app.UseMvc();
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        protected void GetJsonFilesConfiger()
        {
            BaseConfig.ConnectionString = Configuration.GetConnectionString("ConnectionString");
            BaseConfig.RedisConnectionString = Configuration["RedisConnectionString:ConnectionString"];
        }
        /// <summary>
        /// 加载所有程序集
        /// </summary>
        protected void GetAssembly()
        {
            IList<Assembly> ass = new List<Assembly>();
            var lib = DependencyContext.Default;
            var libs = lib.CompileLibraries.Where(t => !t.Serviceable).Where(t => t.Type != "package").ToList();
            foreach (var item in libs)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
                ass.Add(assembly);
            }
            BaseConfig.Assembly = ass;
        }
    }
}