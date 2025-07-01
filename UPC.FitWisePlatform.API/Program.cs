using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Cortex.Mediator.Behaviors;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Lower Case URLs
 builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Agrega esto justo después de crear el builder:
 builder.Services.AddCors(options =>
 {
  options.AddPolicy("PermitirFrontend", policy =>
  {
   policy.WithOrigins("http://localhost:5173") // URL de tu frontend
    .AllowAnyMethod()
    .AllowAnyHeader();
  });
 });

// Configure Kebab Case Route Naming Convention
 builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
 var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Verify if the connection string is not null or empty
 if (string.IsNullOrEmpty(connectionString))
 {
     throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
 }

// Configure Database Context and Logging Level
 if (builder.Environment.IsDevelopment())
  builder.Services.AddDbContext<AppDbContext>(options =>
  {
   options.UseMySQL(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
  });
 else  if (builder.Environment.IsProduction())
  builder.Services.AddDbContext<AppDbContext>(options =>
  {
   options.UseMySQL(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Error)
    .EnableDetailedErrors();
  }); 
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnet/core/swashbuckle
 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(options =>
 {
   options.SwaggerDoc("v1",
    new OpenApiInfo
    {
       Title = "ACME.LearningCenter.Platform.API",
       Version = "v1",
       Description = "ACME LearningCenter Platform API",
       TermsOfService = new Uri("htpps://acme-learning.com/tos"),
       Contact = new OpenApiContact
       {
          Name = "ACME Studios",
          Email = "contact@acme.com"
       },
       License = new OpenApiLicense
       {
         Name = "Apache 2.0",
         Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
       }
    });
   
 });


// Configure Dependency Injection
 
// Shared Bounded Context Injection Configuration
  builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ************* Publishing Bounded Context Injection Configuration
  builder.Services.AddScoped<IHealthPlanRepository, HealthPlanRepository>();
  builder.Services.AddScoped<IHealthPlanCommandService, HealthPlanCommandService>();
  builder.Services.AddScoped<IHealthPlanQueryService, HealthPlanQueryService>();

  builder.Services.AddScoped<IMealRepository, MealRepository>();
  builder.Services.AddScoped<IMealCommandService, MealCommandService>();
  builder.Services.AddScoped<IMealQueryService, MealQueryService>();

  builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
  builder.Services.AddScoped<IExerciseCommandService, ExerciseCommandService>();
  builder.Services.AddScoped<IExerciseQueryService, ExerciseQueryService>();

  builder.Services.AddScoped<IHealthPlanExerciseRepository, HealthPlanExerciseRepository>();
  builder.Services.AddScoped<IHealthPlanExerciseCommandService, HealthPlanExerciseCommandService>();
  builder.Services.AddScoped<IHealthPlanExerciseQueryService, HealthPlanExerciseQueryService>();

  builder.Services.AddScoped<IHealthPlanMealRepository, HealthPlanMealRepository>();
  builder.Services.AddScoped<IHealthPlanMealCommandService, HealthPlanMealCommandService>();
  builder.Services.AddScoped<IHealthPlanMealQueryService, HealthPlanMealQueryService>();


// Mediator Configuration

// Add Mediator Injection Configuration
  builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
  builder.Services.AddCortexMediator(
   configuration: builder.Configuration,
   handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
   {
    options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
   });
  
var app = builder.Build();

// Verify if the database is created and apply migrations
using (var scope = app.Services.CreateScope())
{
   var services = scope.ServiceProvider;
   var context = services.GetRequiredService<AppDbContext>();
   context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//  app.MapOpenApi();
//}

app.UseSwagger();
app.UseSwaggerUI();

// Agrega aquí el middleware CORS
app.UseCors("PermitirFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();