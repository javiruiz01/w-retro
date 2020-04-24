using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using wRetroApi.Models;
using wRetroApi.Repositories;
using wRetroApi.Services;

namespace wRetroApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string AllowedOrigins = "AllowedOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options
                    .AddPolicy(
                        AllowedOrigins,
                        builder => builder
                            .WithOrigins(Configuration["AllowedOrigins"])
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
            });

            services
                .Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)))
                .PostConfigure<DatabaseSettings>(myOptions =>
                    myOptions.ConnectionString += $"Password={Environment.GetEnvironmentVariable("SA_PASSWORD")}");
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddSignalR();
            services.AddHealthChecks()
                .AddSqlServer(Configuration["DatabaseSettings:ConnectionString"]);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowedOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalHub>("/realtime");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}