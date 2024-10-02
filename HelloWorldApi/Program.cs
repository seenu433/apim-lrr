using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
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

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/llrconsumer", async (ILogger<Program> logger, HttpResponse response) =>
{
    // Create an instance of HttpClient (you can reuse this)
    var httpClient = new HttpClient();
    var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    try
    {
        string serverEndpoint = Environment.GetEnvironmentVariable("SERVER_ENDPOINT") ?? "http://localhost:5000";
        // Make the synchronous GET request
        using (var request = new HttpRequestMessage(HttpMethod.Get, serverEndpoint))
        {
            logger.LogInformation("Start time: " + DateTime.Now);

            httpClient.Timeout = TimeSpan.FromMinutes(100);

            var apiResponse = httpClient.Send(request);
            apiResponse.EnsureSuccessStatusCode();

            logger.LogInformation("End time: " + DateTime.Now);

            // Read the response content as a stream
            using var stream = apiResponse.Content.ReadAsStream();
            //convert stream to a string
            var content = new StreamReader(stream).ReadToEnd();

            string jsonString = JsonSerializer.Serialize(content);
            await response.WriteAsync(jsonString);
        }
    }
    catch (Exception ex)
    {
        logger.LogInformation("End time: " + DateTime.Now);
        logger.LogInformation(ex.Message);
    }
});

app.MapGet("/llrserver", async (ILogger<Program> logger, HttpResponse response) =>
{
    logger.LogInformation("Request received...");
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    TimeSpan runDuration = TimeSpan.FromMinutes(40);
    DateTime endTime = DateTime.Now.Add(runDuration);

    while (DateTime.Now < endTime)
    {
        logger.LogInformation("Waiting for weather forecast...");
        // Wait for a short interval before checking again
        await Task.Delay(1000);
    }
    logger.LogInformation("Done waiting...");

    string jsonString = JsonSerializer.Serialize(forecast);
    await response.WriteAsync(jsonString);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
