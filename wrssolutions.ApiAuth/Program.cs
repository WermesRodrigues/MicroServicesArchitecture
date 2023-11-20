using wrssolutions.IoC.Dependency;
using wrssolutions.IoC.Identity;
using wrssolutions.IoC.ServicesDepencency;
using wrssolutions.IoC.SwaggerDependecy;

var builder = WebApplication.CreateBuilder(args);

//Logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddRepositories(configuration);
builder.Services.AddServices(configuration, environment);
builder.Services.AddIdentityConfiguration(configuration);
builder.Services.AddApiVersioning(options =>
{
    //indicating whether a default version is assumed when a client does
    // does not provide an API version.
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;   // this is needed to work
});
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseApiVersioning();
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
    //options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v2");
    options.RoutePrefix = "docs";

});

app.Run();




