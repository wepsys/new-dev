using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json.Serialization;
using Wepsys.DianaHr.Core.Repository;
using Wepsys.DianaHr.Core.Services;
using Wepsys.DianaHr.Core.Services.Contracts;
using Wepsys.DianaHr.Persistence;
using Wepsys.DianaHr.Persistence.Repositories;
using Wepsys.DianaHr.WebApi.ExceptionHandler;

namespace Wepsys.DianaHr.WebApi
{
    /// <summary>
    /// Program initialization
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        private const string OriginsKey = "Origins";
        private const string SqlConnectionString = "SqlServerConnection";

        /// <summary>
        /// Application main method
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            AddCors(builder);
            AddControllers(builder);
            AddCompressionMethod(builder);
            AddDbContext(builder);
            // Add services to the container.
            AddRepositories(builder);
            AddServices(builder);
            
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureExceptionHandler();

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseResponseCompression();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });

            // migrate any database changes on startup (includes initial db creation)
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<WepsysDianaHrContext>();
                dataContext.Database.Migrate();
            }

             await app.RunAsync();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
        }

        private static void AddRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddCors(WebApplicationBuilder builder)
        {
            var origins = builder.Configuration.GetSection(OriginsKey).Get<string[]>();

            builder.Services.AddCors(c
                => c.AddDefaultPolicy(builder
                    => builder
                        .WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));
        }

        private static void AddControllers(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions((opts) =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        private static void AddCompressionMethod(WebApplicationBuilder builder)
        {
            builder.Services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
        }

        private static void AddDbContext(WebApplicationBuilder builder)
        {
            var migrationsAssembly = typeof(WepsysDianaHrContext).GetTypeInfo().Assembly.GetName().Name;
            string connectionString = builder.Configuration.GetConnectionString(SqlConnectionString);

            void ContextBuilder(DbContextOptionsBuilder b) =>
                b.UseSqlServer(connectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                    sql.MigrationsHistoryTable("_EFDianaHrMigrationHistory", "dianahr");
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

            builder.Services.AddDbContext<WepsysDianaHrContext>(ContextBuilder);
            builder.Services.AddScoped<DbContext, WepsysDianaHrContext>();
        }
    }
}