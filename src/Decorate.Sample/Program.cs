using System;
using BuffDecoraters.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Decorate.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            AddServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<ITestService>().Test("Test");
            serviceProvider.GetService<ITestService>().PipeTest("PipeTest");
            Console.ReadLine();
        }


        private static  void AddServices(IServiceCollection serviceCollection)
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger = log;
            serviceCollection.AddSingleton<ILogger>(log);

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();
            var assmbly = typeof(BTestAttribute).Assembly;
            //serviceCollection.AddMethodAttributeDecorated(typeof(BTestAttribute), assmbly, assmbly);
            serviceCollection.AddMethodAttributeDecorated(typeof(ATestAttribute), assmbly, assmbly);
        }
    }
}
