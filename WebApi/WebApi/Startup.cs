using System;
using Fullstack.Data.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddControllers();
      services.AddDbContext<PropertyListingContext>(opt =>
             opt.UseSqlServer(Configuration.GetConnectionString("PropertyListingConnex"))
            .EnableSensitiveDataLogging()
           );

      // configure strongly typed settings object
      services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

      services.AddScoped<IFullStackRepository, FullStackRepository>();

      // configure DI for application services
      services.AddScoped<IAdvertService, AdvertService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<ISellerService, SellerService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseRouting();

      // global cors policy
      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

      // custom jwt auth middleware
      app.UseMiddleware<JwtMiddleware>();

      app.UseEndpoints(x => x.MapControllers());
    }
  }
}
