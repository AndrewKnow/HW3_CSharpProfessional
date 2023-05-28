using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Abstractions;
using WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            var builder = WebApplication.CreateBuilder();

            builder.Services.AddDbContext<DataContext>(x =>
            {
                //���������, ��� ���������� PSQL
                //���������, ��� ������ ����������� �� ������������
                x.UseNpgsql(builder.Configuration.GetConnectionString("db"));

                x.UseSnakeCaseNamingConvention();
            });

            // ������������� ���� �������������� ���������� ���������
            builder.Services.AddScoped(typeof(DbContext), typeof(DataContext));
            //������������ �����������
            builder.Services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository));
            // ������������ ��������, �������� �� �� ����. ��� �������� ���� ��� � powershell
            // cmd ����� "dotnet tool install --global dotnet - ef--version 6.*"
            // ����� "powershell" � ����� ������� � ��� "dotnet ef migrations add InitialCreate" ����� "dotnet ef database update"
            // dotnet ef migrations remove
            var app = builder.Build();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}