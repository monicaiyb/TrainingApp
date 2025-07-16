using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrainingApp.BLL.Interfaces;
using TrainingApp.BLL.Services;
using TrainingApp.Data;
using TrainingApp.Data.Models.Users;
using TrainingApp.Data.Repository;
using TrainingApp.Data.SeedData;
using TrainingApp.Middleware;


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
builder.Services.AddTransient<IBackgroundWorkflowService, BackgroundWorkflowService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<Role>>();


// Add SeedData
builder.Services.AddTransient<SeedData>();

builder.Services.AddIdentity<ApplicationUser, Role>(

        options => options.SignIn.RequireConfirmedAccount = true
    )

    .AddEntityFrameworkStores<TrainingContextDb>().AddRoles<Role>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//}); 






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["token:audience"],
        ValidIssuer = builder.Configuration["token:issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["token:key"]))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddHangfire(config =>
    {
        var options = new SqlServerStorageOptions
        {
            PrepareSchemaIfNecessary = true,
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(2),  
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(2),
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        };
        config.UseSqlServerStorage(connectionString, options);
    }
       
    );
builder.Services.AddHangfireServer();

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
//app.MapGet("/login", [AllowAnonymous] () => "This endpoint is for all roles.");
app.MapGet("/", () => "hello world");
app.UseAuthorization();
//app.UseHangfireDashboard();


//app.UseHangfireServer();

//RecurringJob.AddOrUpdate<BackgroundWorkflowService>("Updating WOrkflow Information", r => r.UpdateWorkflowInfo(), Cron.Weekly);


app.MapControllers();

app.Run();
