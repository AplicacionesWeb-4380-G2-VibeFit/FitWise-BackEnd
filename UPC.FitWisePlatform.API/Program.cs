using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

using Cortex.Mediator.Behaviors;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;

// Publishing
using UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

// Reviewing
using UPC.FitWisePlatform.API.Reviewing.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Reviewing.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;
using UPC.FitWisePlatform.API.Reviewing.Infrastructure.Persistence.EFC.Repositories;

// Organizing
using UPC.FitWisePlatform.API.Organizing.Application.Internal.CommandServices;
using UPC.FitWisePlatform.API.Organizing.Application.Internal.QueryServices;
using UPC.FitWisePlatform.API.Organizing.Domain.Repositories;
using UPC.FitWisePlatform.API.Organizing.Domain.Services;
using UPC.FitWisePlatform.API.Organizing.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Kebab-case route naming
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// DB connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    });
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Error)
               .EnableDetailedErrors();
    });
}

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ACME.LearningCenter.Platform.API",
        Version = "v1",
        Description = "ACME LearningCenter Platform API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
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

// Dependency Injection

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Publishing
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

// ************* Reviewing Bounded Context Injection Configuration *************

// Repositories
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewCommentRepository, ReviewCommentRepository>();
builder.Services.AddScoped<IReviewReportRepository, ReviewReportRepository>();

// Command Services
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewCommentCommandService, ReviewCommentCommandService>();
builder.Services.AddScoped<IReviewReportCommandService, ReviewReportCommandService>();

// Query Services
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();
builder.Services.AddScoped<IReviewCommentQueryService, ReviewCommentQueryService>();
builder.Services.AddScoped<IReviewReportQueryService, ReviewReportQueryService>();

// Mediator
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) },
    configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
    });

// ************* Organizing Bounded Context Injection Configuration *************
// Repositories
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
// Command Services
builder.Services.AddScoped<IScheduleCommandService, ScheduleCommandService>();
// Query Services
builder.Services.AddScoped<IScheduleQueryService, ScheduleQueryService>();

var app = builder.Build();

// DB initialization
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("PermitirFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
