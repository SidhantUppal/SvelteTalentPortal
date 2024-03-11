using TalentPortal.BAL.Interfaces;
using TalentPortal.BAL.Services;
using TalentPortal.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<IDsrService, DsrService>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<ILeaveService, LeaveService>();
builder.Services.AddTransient<IReportService, ReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
