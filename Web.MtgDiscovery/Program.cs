using System;
using System.Diagnostics;
using Azure.Identity;
using Lib.UniversalCore.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Web.MtgDiscovery;

public sealed class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<StartUp>();
            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("local.settings.json", true)
                    .AddEnvironmentVariables();
                IConfigurationRoot confRoot = builder.Build();
                builder.AddAzureAppConfiguration(options =>
                {
                    options.Connect(AzureAppConfigConStr(confRoot))
                        .ConfigureKeyVault(kv =>
                        {
                            kv.SetCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions
                            {
                                Retry =
                                {
                                    // Reduce retries and timeouts to get faster failures
                                    MaxRetries = 2,
                                    NetworkTimeout = TimeSpan.FromSeconds(7),
                                    MaxDelay = TimeSpan.FromSeconds(7)
                                }
                            }));
                        })
                        ;
                });
            })
            .ConfigureServices((context, services) =>
            {
                MonoStateConfig.SetConfiguration(context.Configuration);
            });

    private static string AzureAppConfigConStr(IConfiguration confRoot)
    {
        string colon = confRoot["AppConfiguration:ConnectionString"];
        string dot = confRoot["AppConfiguration.ConnectionString"];
        string underscore = confRoot["AppConfiguration_ConnectionString"];
        if (string.IsNullOrWhiteSpace(colon) is false) return colon;
        if (string.IsNullOrWhiteSpace(dot) is false) return dot;
        if (string.IsNullOrWhiteSpace(underscore) is false) return underscore;

        throw new UnreachableException($"One of these should have been set [colon={colon}] [dot={dot}] [underscore={underscore}]");
    }
}
