using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApiProdutosPessoas.Data;
using ApiProdutosPessoas.Authentication;
using ApiProdutosPessoas.Middleware;
using ApiProdutosPessoas.Repositories.Interfaces;
using ApiProdutosPessoas.Repositories;

namespace ApiProdutosPessoas
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
            // Configuração do DbContext
            services.AddDbContext<ProdutosPessoasdbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Database")));

            // Configuração da autenticação JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            // Serviço para geração de tokens
            services.AddScoped<TokenService>();

            services.AddScoped<InterfaceCidade, CidadeRepositorio>();
            services.AddScoped<InterfaceDependente, DependenteRepositorio>();
            services.AddScoped<InterfaceMarca, MarcaRepositorio>();
            services.AddScoped<InterfacePessoa, PessoaRepositorio>();
            services.AddScoped<InterfaceProduto, ProdutoRepositorio>();

            // Adicionando controllers
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Produtos e Pessoas API", Version = "v1" });

                // Configuração para autorização via JWT no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Produtos e Pessoas API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Middleware de autenticação e autorização
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Middleware de tratamento de erros
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
