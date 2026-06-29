var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();              // Enables Controllers
builder.Services.AddEndpointsApiExplorer();     // Enables Swagger endpoints
builder.Services.AddSwaggerGen();               // Enables Swagger UI

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

//  Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Enable Swagger JSON
    app.UseSwaggerUI();      // Enable Swagger UI
}

// Optional (you can keep or remove later)
app.UseHttpsRedirection();

//  VERY IMPORTANT: Map controllers
app.MapControllers();

app.Run();