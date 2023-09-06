using Microsoft.EntityFrameworkCore;
using PrinzipMonitorService.DAL.ApplicationContext.MsSql;
using PrinzipMonitorService.DAL.Repositories.FlatRepository;
using PrinzipMonitorService.DAL.Repositories.UserRepository;

namespace PrinzipMonitorService
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IFlatRepository, FlatRepository>();

            string msSqlConnectionString = Configuration.GetConnectionString("MsConnection");

            services.AddDbContext<MsSqlDbContext>(options => options.UseSqlServer(msSqlConnectionString));

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
