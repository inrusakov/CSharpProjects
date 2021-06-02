using HSEApiTraining.Models.Options;
using HSEApiTraining.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using HSEApiTraining.Bot;

namespace HSEApiTraining
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {           
            //Необходимо подключить конфиг, в котором содержится строка подключения к БД 
            services.Configure<DbConnectionOptions>(Configuration.GetSection(nameof(DbConnectionOptions)));

            //Подключаем провайдер подключения 
            services.AddSingleton<ISQLiteConnectionProvider, SQLiteConnectionProvider>();

            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IMessageRepository, MessageRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Naval Combat",
                    Version = "Русаков Иван" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HSE Training App");
                c.RoutePrefix = "swagger";
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            //Bot Configurations
            Bot.Bot.GetBotClientAsync().Wait();
        }
    }
}
