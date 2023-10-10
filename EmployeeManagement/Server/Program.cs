using EmployeeManagement.Server.Manager;
using EmployeeManagement.Server.Repository;
using EmployeeManagement.Data.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("EmployeeAppCon");
builder.Services.AddDbContext<EmployeeContext>(option => option.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeManager, EmployeeManager>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddControllers(); 
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors
//(c =>
//{
//    var policyName = "EmployeeManagement";
//    c.AddPolicy(name: policyName,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://localhost:5001", "http://localhost:4200/");
//                      });
//});

//Configure Serilog
var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.NVarChar)
                 }
};
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
    .Enrich.FromLogContext()
    .WriteTo.File("../Logs/log.txt", rollingInterval: RollingInterval.Day)
    //.WriteTo.File(path: "Logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate:"{Timestamp: yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}")
    .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "Log" }
               , null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
    .CreateLogger();

//Use Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagement");
});

app.Run();
