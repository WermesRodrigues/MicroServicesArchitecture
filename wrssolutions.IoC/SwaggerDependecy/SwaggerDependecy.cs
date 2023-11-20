using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace wrssolutions.IoC.SwaggerDependecy
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                  new OpenApiInfo
                  {
                      Title = "wrssolutions",
                      Description = "API wrssolutions for test dev",
                      Version = "1",
                      Contact = new OpenApiContact()
                      {
                          Name = "Wermes Rodrigues",
                          Url = new Uri("https://github.com/wermesrodrigues"),
                      }
                  });

                c.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Title = "wrssolutions",
                        Description = "API wrssolutions for test dev",
                        Version = "2",
                        Contact = new OpenApiContact()
                        {
                            Name = "Wermes Rodrigues",
                            Url = new Uri("https://github.com/wermesrodrigues"),
                        }
                    });

                c.OperationFilter<SwaggerDefault>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter with token JWT: Bearer {token} ",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });
            return services;
        }
    }



    public class SwaggerDefault : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }
            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }
                parameter.Required |= description.IsRequired;
            }
        }
    }

    public class SwaggerAuth
    {
        private readonly RequestDelegate next;

        public SwaggerAuth(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/docs") && (context.User.Identity != null && !context.User.Identity.IsAuthenticated))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            await next.Invoke(context);
        }
    }

}
