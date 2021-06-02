using HSEApiTraining.Models.Options;
using HSEApiTraining.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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

            //Тут нужно подключать уже созданные сервисы. 
            //Они могут быть 3 типов Singleton, Transient, Scoped
            //Делайте для простоты Singleton, в других и их различиях при необходимости разберемся позже

            // Подключение нового сервиса DummyService.
            services.AddSingleton<IDummyService, DummyService>();
            //Добавляем новый сервис
            services.AddSingleton<ICalculatorService, CalculatorService>()
                .AddSingleton<ICustomerService, CustomerService>()
                .AddSingleton<IBanService, BanService>();

            //Подключаем провайдер подключения 
            services.AddSingleton<ISQLiteConnectionProvider, SQLiteConnectionProvider>();

            //Добавляем репозиторий (сущность, отвечающая за работу с данными, т.н. Data Access Layer
            services.AddSingleton<ICustomerRepository, CustomerRepository>()
                .AddSingleton<IBanRepository, BanRepository>();

            // Подключение сервиса CurrencyService.
            services.AddSingleton<ICurrencyService, CurrencyService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HSE Training App",
                    Version = "Русаков Иван и Пак Марк" });
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
        }
    }
}
