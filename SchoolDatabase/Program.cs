using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolDatabase.Context;
using SchoolDatabase.Middleware;
using SchoolDatabase.Model.Entity.User;
using SchoolDatabase.Options;
using SchoolDatabase.Services;
using SchoolDatabase.UnitOfWork;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container
builder.Services.AddRazorPages();

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITeacherUnitOfWork, TeacherUnitOfWork>();
builder.Services.AddScoped<IStudentUnitOfWork, StudentUnitOfWork>();
builder.Services.AddScoped<ICourseUnitOfWork, CourseUnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = true;
})
                .AddEntityFrameworkStores<SchoolAPIDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
}); ;

#region Swagger

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolDatabaseAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

#endregion

#region Db

var dbContextOptions = new DbConnectionOption();
builder.Services.Configure<DbConnectionOption>(
    builder.Configuration.GetSection(DbConnectionOption.ConnectionStrings)
);
builder.Configuration.GetSection(DbConnectionOption.ConnectionStrings).Bind(dbContextOptions);

//Add custom TrainCarAPIDbContext "service" to the container.
builder.Services.AddDbContext<SchoolAPIDbContext>(options =>
{
    var dbBuilder = options.UseSqlServer(dbContextOptions.SchoolDatabaseAPIDb);
    if (builder.Environment.IsDevelopment())
    {
        dbBuilder.EnableSensitiveDataLogging();
    }
});

#endregion

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<RequestResponseMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// custom endpoint
app.MapGet("/hi", () => "Hello!");

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
