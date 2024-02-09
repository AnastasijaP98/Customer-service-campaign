using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/FileUpload", async (IFormFile file) =>
{
    string tempfile = CreateTempfilePath();
    using var stream = File.OpenWrite(tempfile);
    await file.CopyToAsync(stream);

    
});

app.MapPost("/FileUploadMany", async (IFormFileCollection myFiles) =>
{
    foreach (var file in myFiles)
    {
        string tempfile = CreateTempfilePath();
        using var stream = File.OpenWrite(tempfile);
        await file.CopyToAsync(stream);

        
    }
});

static string CreateTempfilePath()
{
    var filename = $"{Guid.NewGuid()}.tmp";
    var directoryPath = Path.Combine("Files", "uploads");
    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

    return Path.Combine(directoryPath, filename);
}


app.Run();




