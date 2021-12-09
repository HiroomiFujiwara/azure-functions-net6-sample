using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Sample.UseCases.Users;

namespace MyCompany.Sample.Presentations.Functions
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
                .ConfigureOpenApi()
                .ConfigureServices(services =>
                {
                    services.AddTransient<IGetUserUseCase, GetUserUseCase>();
                })
                .Build();

            host.Run();
        }
    }
}