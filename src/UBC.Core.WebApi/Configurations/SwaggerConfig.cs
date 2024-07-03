using Microsoft.OpenApi.Models;

namespace UBC.Core.WebApi.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMvcCore().AddApiExplorer();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                s.SwaggerDoc
                (
                    "v1"
                    , new OpenApiInfo
                    {
                        Version = configuration["AppSettings:Application:Version"],
                        Title = "App Desafio UBC - Core API",
                        Description = "Aplicação de teste: Desafio UBC Core API Swagger",
                        Contact = new OpenApiContact
                        {
                            Name = "App Desafio UBC",
                            Email = "tatianeo.sistemas@gmail.com"
                        }
                    }
                );

                s.CustomSchemaIds(x => x.FullName.Replace("+", "_"));
                s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        /// <summary>
        /// Método de chamada no app builder do Swagger Configuration 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio UBC - Core API");
                c.RoutePrefix = "";
            });

            return app;
        }
    }
}
