using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Test;

public class TestingWebAppFactory : WebApplicationFactory<Program> {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            // SS: remove production dbcontext
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TeacherDbContext>));
            if (descriptor != null) {
                services.Remove(descriptor);
            }

            // SS: use sqlite in-memory context
            services.AddDbContext<TeacherDbContext>(option => {
                // SS: setup in-memory database that is shared between connections
                option.UseSqlite("DataSource=file::memory:?cache=shared")
                    .EnableSensitiveDataLogging();
            });

            // SS: create in-memory database
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<TeacherDbContext>()) {
                try {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex) {
                    throw;
                }
            }
        });
    }

}