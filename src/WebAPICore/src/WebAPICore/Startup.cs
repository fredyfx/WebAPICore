using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using TestCoreAPI.Controllers;

namespace TestCoreAPI
{
  public class Startup
  {
    private IHostingEnvironment _environment;
    IConfigurationRoot _config;

    public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
    {
      // Set up configuration sources.
      var builder = new ConfigurationBuilder()
          .SetBasePath(appEnv.ApplicationBasePath)
          .AddJsonFile("config.json")
          .AddEnvironmentVariables();

      _config = builder.Build();

      _environment = env;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Save my dummy data to the DI Container
      services.AddInstance(typeof(People), new People());

      // Add framework services.
      services.AddMvc(config =>
      {
        // Add XML Content Negotiation
        config.RespectBrowserAcceptHeader = true;
        config.InputFormatters.Add(new XmlSerializerInputFormatter());
        config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
      })
        .AddJsonOptions(opts =>
        {
          // Force Camel Case to JSON
          opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        loggerFactory.AddDebug(LogLevel.Information);
      }
      else
      {
        loggerFactory.AddDebug(LogLevel.Error);
      }

      loggerFactory.AddConsole(_config.GetSection("Logging"));

      app.UseStaticFiles();

      app.UseMvc();
    }

    // Entry point for the application.
    public static void Main(string[] args) => WebApplication.Run<Startup>(args);
  }
}
