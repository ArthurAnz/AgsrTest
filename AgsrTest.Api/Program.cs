using AgsrTest.Api.Domain.Interfaces.Repositories;
using AgsrTest.Api.Domain.Interfaces.Services;
using AgsrTest.Api.Infrastructure;
using AgsrTest.Api.Infrastructure.Repositories;
using AgsrTest.Api.Infrastructure.Swagger;
using AgsrTest.Api.Middlewares;
using AgsrTest.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<PatientSchemaFilter>();
});
builder.Services.AddDbContext<AgsrTestDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IStoredProcedureService, StoredProcedureService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AgsrTestDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/healthcheck");

app.MapControllers();

app.Run();
