using JobSyncWebApi.Models;
using JobSyncWebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<JobContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("JobConnection")));
builder.Services.AddScoped<IJobRepository,SQLJobRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Add your React app's URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
            //For any origin not suggested for production
            //policy.AllowAnyOrigin()
            //  .AllowAnyHeader()
            //  .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
