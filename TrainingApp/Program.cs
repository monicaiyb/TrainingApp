

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainingApp.BLL.Interfaces;
using TrainingApp.BLL.Services;
using TrainingApp.Data;
using TrainingApp.Data.Models.Users;
using TrainingApp.Data.Repository;
using TrainingApp.Data.SeedData;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json");
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'TrainingContextDbConnection' not found.");
builder.Services.AddDbContextPool<TrainingContextDb>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Number of retries
            maxRetryDelay: TimeSpan.FromSeconds(30), // Max wait time between retries
            errorNumbersToAdd: null // Use default transient errors
        );
    })

);

// Add services to the container.

builder.Services.AddTransient<IDbRepository, DbRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IWorkflowService, WorkflowService>();

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<Role>>();


// Add SeedData
builder.Services.AddTransient<SeedData>();

builder.Services.AddIdentity<ApplicationUser, Role>(

        options => options.SignIn.RequireConfirmedAccount = true
    )

    .AddEntityFrameworkStores<TrainingContextDb>().AddRoles<Role>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

builder.Services.Configure<IdentityOptions>(options =>
{
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();


builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

//Seed Data
SeedDatabase();



void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();

            var scopedContext = services.GetRequiredService<SeedData>();
            SeedData.SeedDataAndRole(userManager, roleManager);
        }
        catch (Exception ex)
        {
            // Handle exception appropriately, e.g., log it
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            throw;
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/login", [AllowAnonymous] () => "This endpoint is for all roles.");

app.UseAuthorization();

app.MapControllers();

app.Run();
