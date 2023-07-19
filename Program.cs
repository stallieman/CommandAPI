using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CommandAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

var services = builder.Services;

var npgsqlBuilder = new NpgsqlConnectionStringBuilder();
npgsqlBuilder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");

services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(npgsqlBuilder.ConnectionString));

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => 
{
    opt.Audience = Configuration["ResourceID"];
    opt.Authority = $"{Configuration["Instance"]}{Configuration["TenantId"]}";
});

services.AddControllers().AddNewtonsoftJson(s => 
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();