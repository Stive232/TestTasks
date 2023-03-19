using Microsoft.OpenApi.Models;
using System.Reflection;
using TestProject.API.Infrastructure;
using TestProject.Logic.Services;
using TestProject.Logic.Services.Document;
using TestProject.Repositories;
using TestProject.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TestProject API",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<DocumentsData>();
builder.Services.AddTransient<IDbDocumentRepository, DbDocumentRepository>();
builder.Services.AddTransient<IDocumentService, DocumentService>();

var app = builder.Build();


if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseRouting();
app.UseEndpoints(endpoints =>  endpoints.MapControllers());
app.Run();