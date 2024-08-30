using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentApi;
using StudentApi.Authorization;
using StudentApi.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<attachmentOptions>(builder.Configuration.GetSection("Attachments"));

//var attachmentOptions = builder.Configuration.GetSection("Attachments").Get<attachmentOptions>();
//builder.Services.AddSingleton(attachmentOptions);


//var attachmentOptions = new attachmentOptions();
//builder.Configuration.GetSection("Attachments").Bind(attachmentOptions);
//builder.Services.AddSingleton(attachmentOptions);

builder.Services.AddDbContext<ApplicationDbcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
); 


// Add services to the container.


//builder.Services.Configure<ApplicationDbcontext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers(options=>
{
    options.Filters.Add<PermissionBasedAuthorizationFilter>();

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);


builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options=>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
