using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EPAS.API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EPAS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
       

            #region Swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1.0.0", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1.0.0",
                    Title = " EPAS.API接口",
                    Description = "by WHW 研发团队"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
                option.OperationFilter<GlobalHttpHeaderOperationFilter>(); // 添加httpHeader参数
                option.CustomSchemaIds(x => x.FullName);
            });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           
            services.AddMvc().AddJsonOptions(options => {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //保证Json中实体字段首字母 不小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
  
            //可以访问根目录下面的静态文件
            var staticfile = new StaticFileOptions { FileProvider = new PhysicalFileProvider(AppContext.BaseDirectory) };
            app.UseStaticFiles(staticfile);

            //todo:测试可以允许任意跨域，正式环境要加权限
            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0.0/swagger.json", "EPAS.API V1.0.0 Docs");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
