using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderEmailHandler.Interface;
using OrderEmailHandler.Models;
using OrderEmailHandler.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.AddLogging();
    })
    .ConfigureServices((context, services) =>
    {
        // Bind EmailSettings
        services.Configure<EmailSettings>(context.Configuration.GetSection("EmailSettings"));

        // Register EmailService
        services.AddSingleton<IEmailService, EmailService>();
    })
    .Build();



host.Run();