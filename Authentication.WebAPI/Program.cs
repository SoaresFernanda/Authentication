using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Authentication.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                 };
             });

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebAPI",
        Version = "v1",
        Description = "Authentication User API"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Autenticação da Integração. \r\n\r\n Digite o 'Bearer' [incluir espaço] e" +
        " em seguida o token gerado na autenticação.\r\n\r\nExemplo: \"Bearer tokenGerado\"",
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
    c.OperationFilter<SwaggerCategorizeFilter>();

    c.DocumentFilter<SwaggerOrderOperationFilter>();
});
builder.Services
    .AddScoped<IServiceCollection, ServiceCollection>();

builder.Services
   .AddHttpContextAccessor();
    // .AddServices()
    //.AddNHibernate()
    //.AddLogger();
        


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("EnableCORS");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseExceptionHandler(
    options =>
        {
            options.Run(
                async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (null != exceptionObject)
                    {
                        var errorMessage = $"<b>Error: {exceptionObject.Error.Message} </b > {exceptionObject.Error.StackTrace} ";
                        await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                    }
                });
        }
    );

