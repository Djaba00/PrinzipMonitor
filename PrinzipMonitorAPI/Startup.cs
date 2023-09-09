using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrinzipMonitorService.Configurations;
using PrinzipMonitorService.DAL.ApplicationContext.MsSql;
using PrinzipMonitorService.DAL.Repositories.FlatRepository;
using PrinzipMonitorService.DAL.Repositories.UserRepository;

namespace PrinzipMonitorService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            string msSqlConnectionString = Configuration.GetConnectionString("MsConnection");

            services.AddDbContext<MsSqlDbContext>(options => options.UseSqlServer(msSqlConnectionString).LogTo(Console.WriteLine, LogLevel.None))
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IFlatRepository, FlatRepository>();

            services.AddControllers();
                //.AddNewtonsoftJson(options =>
                //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
