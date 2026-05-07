using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace SME.SERAp.Prova.Item.Api.Configurations
{
    public static class RegistraDocumentacaoSwagger
    {
        public static void Registrar(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SERAp Cadastro de Itens API", Version = "1.0" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Para autenticação, incluir 'Bearer' seguido do token JWT. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                    }
                );
            });
        }
    }
}