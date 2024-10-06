using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Test;

public class TestingWebAppFactory : WebApplicationFactory<Program> {
    private SqliteConnection _keepAliveConnection;

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            // SS: remove production dbcontext
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TeacherDbContext>));
            if (descriptor != null) {
                services.Remove(descriptor);
            }

            var connectionString = "Data Source=TeacherDb;Mode=Memory;Cache=Shared";
            connectionString = "DataSource=file::memory:?cache=shared";

            // SS: use sqlite in-memory context
            services.AddDbContext<TeacherDbContext>(option => {
                option.UseSqlite(connectionString)
                    .EnableSensitiveDataLogging();
            });

            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<TeacherDbContext>()) {
                try {
                    appContext.Database.EnsureCreated();

                    _keepAliveConnection = new SqliteConnection(connectionString);
                    _keepAliveConnection.Open();
                }
                catch (Exception ex) {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        });
    }

}