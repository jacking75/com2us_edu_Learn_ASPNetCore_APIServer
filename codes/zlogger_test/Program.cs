using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();

SettingLogger();

WebApplication app = builder.Build();


//log setting
ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
LogManager.SetLoggerFactory(loggerFactory, "Global");




app.UseRouting();
#pragma warning disable ASP0014
app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });
#pragma warning restore ASP0014

app.Run(configuration["ServerAddress"]);



void SettingLogger()
{
    ILoggingBuilder logging = builder.Logging;
    _ = logging.ClearProviders();

    string fileDir = configuration["logdir"];

    bool exists = Directory.Exists(fileDir);

    if (!exists)
    {
        _ = Directory.CreateDirectory(fileDir);
    }

    _ = logging.AddZLoggerRollingFile(
        options =>
        {            
            options.UseJsonFormatter();
            options.FilePathSelector = (timestamp, sequenceNumber) => $"{fileDir}{timestamp.ToLocalTime():yyyy-MM-dd}_{sequenceNumber:000}.log";
            options.RollingInterval = ZLogger.Providers.RollingInterval.Day;
            options.RollingSizeKB = 1024;            
        }); 

    _ = logging.AddZLoggerConsole(options =>
    {
        options.UseJsonFormatter();
    });

}