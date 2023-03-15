
using System.Text;
using TestProject.Logic.Services;
using TestProject.Logic.Services.Document;
using TestProject.Logic.Services.Document.Models;
using TestProject.Repositories;
using TestProject.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<DocumentsData>();
builder.Services.AddTransient<IDbDocumentRepository, DbDocumentRepository>();
builder.Services.AddTransient<IDocumentService, DocumentService>();

var app = builder.Build();



app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseEndpoints(endpoints =>  endpoints.MapControllers());

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/upload" && request.Method == "POST")
    {
        IFormFileCollection files = request.Form.Files;

        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
       
        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            string fullPath = $"{uploadPath}/{file.FileName}";

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string textFromFile;
            using (var fileStream = File.OpenRead(fullPath))
            {
                byte[] buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, buffer.Length);
                textFromFile = Encoding.Default.GetString(buffer);
            }

            if(!string.IsNullOrEmpty(textFromFile))
            {
                List<DocumentModel> documents = new();
                var array = textFromFile.Split("\r");
                foreach (var item in array)
                {
                    DocumentModel model = new();
                    var tmpArray = item.Split("--");
                    for (int i = 0; i < tmpArray.Length; i++)
                    {
                        switch(i)
                        {
                            case 0 :
                                model.LeadId = tmpArray[0].Trim(' ').Replace("\n", "");
                                break;
                            case 1 :
                                model.LastName = tmpArray[1].Trim(' ').Replace("\n", "");
                                break;
                            case 2:
                                model.FirstName = tmpArray[2].Trim(' ').Replace("\n", "");
                                break;
                            case 3:
                                model.ContractNumber = tmpArray[3].Trim(' ').Replace("\n", "");
                                break;
                            case 4:
                                model.WithdrawalAmount = Convert.ToDecimal(tmpArray[4].Trim(' ').Replace("\n", ""));
                                break;
                        }
                    }
                    documents.Add(model);
                }

                var documentService = app.Services.GetService<IDocumentService>();

                documentService.Insert(documents);
            }            
        }
        await response.WriteAsync("Файлы успешно загружены");
    }
    else
    {
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();
