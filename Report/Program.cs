var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddJwtBearer();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ReportDow.Service.CustomerService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/execute", (ReportDow.Service.CustomerService service) =>
{
    try
    {
        service.ExecuteService();
        return Results.Ok("Success");
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Error: {ex.Message}");
    }
});

app.Run();



