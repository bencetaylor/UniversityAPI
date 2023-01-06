using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ICourseService, CourseService>();

#region Db

//Add custom TrainCarAPIDbContext "service" to the container.
builder.Services.AddDbContext<SchoolAPIDbContext>(options =>
{
    var dbBuilder = options.UseSqlServer(@"Server=(local);Database=SchoolDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
    if (builder.Environment.IsDevelopment())
    {
        dbBuilder.EnableSensitiveDataLogging();
    }
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// custom endpoint
app.MapGet("/hi", () => "Hello!");

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
