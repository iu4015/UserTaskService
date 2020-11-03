using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UserTasksService.Data;
using UserTasksService.Security;

namespace UserTasksService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Example API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });

                //  options.SwaggerDoc("v1", new Info { Title = "GAFE API", Version = "v1" });

                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Flow = "implicit",
                //    AuthorizationUrl = configuration.AuthorizationUrl,
                //    Scopes = new Dictionary<string, string>
                //    {
                //        {"gafeAPI", "GAFE API"}
                //    }
                //});

                //options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             // ��������, ����� �� �������������� �������� ��� ��������� ������
                             ValidateIssuer = true,
                             // ������, �������������� ��������
                             ValidIssuer = AuthOptions.ISSUER,

                             // ����� �� �������������� ����������� ������
                             ValidateAudience = true,
                             // ��������� ����������� ������
                             ValidAudience = AuthOptions.AUDIENCE,
                             // ����� �� �������������� ����� �������������
                             ValidateLifetime = true,

                             // ��������� ����� ������������
                             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                             // ��������� ����� ������������
                             ValidateIssuerSigningKey = true,
                         };
                     });

            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<ITasksRepository, TasksRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "GAFE API");

                //const string audienceParamName = "audience";

                options.OAuthAppName("GAFE Swagger UI");
                // options.OAuthClientId(configuration.AuthClientId);
                //options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
                //{
                //    {audienceParamName, configuration.AuthAudience}
                //});

                options.InjectJavascript("StaticFiles/SwaggerInit.js");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
