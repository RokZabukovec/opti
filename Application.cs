using Flock.Cli;
using Flock.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Cli;
using Spectre.Console;

namespace Flock
{
    public class Application
    {
        public int Run(string[] args, IServiceCollection services)
        {
            if (args.Length == 0)
            {
                AnsiConsole.Write(new FigletText("Flock")
                    .LeftAligned()
                    .Color(Color.Blue));
            }
            
            var registrar = new TypeRegistrar(services);

            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                config.AddCommand<Login>("login")
                    .WithDescription("Login with Lina account.");
                
                config.AddCommand<Search>("search")
                    .WithDescription("Search for commands.");
            });

            return app.Run(args);
        }
    }
}
