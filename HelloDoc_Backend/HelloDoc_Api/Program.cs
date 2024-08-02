using HelloDoc_Api.Extensions;
using HelloDoc_Api.Middlewares;
using HelloDoc_BusinessAccessLayer.Helpers;
using HelloDoc_Common.Constants;
using HelloDoc_DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.RegisterDatabaseConnection(builder.Configuration);

builder.Services.AddControllers()
     .ConfigureApiBehaviorOptions(options =>
     {
         options.SuppressModelStateInvalidFilter = true;
     });

builder.Services.AddScoped<JwtTokenHelper>();

builder.Services.ConfigureSwagger(config);

builder.Services.RegisterUnitOfWork();

builder.Services.RegisterServices();

builder.Services.RegisterAutoMapper();

builder.Services.SetRequestBodySize();

builder.Services.ConfigureCors();

builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.RegisterMail(builder.Configuration);

WebApplication? app = builder.Build();

//auto migration
using (IServiceScope? scope = app.Services.CreateScope())
{
    AppDbContext? dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(SystemConstants.CORS_POLICY);

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();