using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebAPI.Test;

public class TestingWebAppFactory : WebApplicationFactory<Program> {

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            // SS: remove production dbcontext
            services.RemoveAll(typeof(DbContextOptions<TeacherDbContext>));

            // SS: create a new SQLite connection that will be used by the DbContext
            var connection = new SqliteConnection("DataSource=file::memory:?cache=shared");
            connection.Open();

            // SS: use sqlite in-memory context
            services.AddDbContext<TeacherDbContext>(option => {
                // SS: setup in-memory database that is shared between connections
                option.UseSqlite(connection)
                    .EnableSensitiveDataLogging();
            });

            // SS: create in-memory database
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope()) {
                var appContext = scope.ServiceProvider.GetRequiredService<TeacherDbContext>();
                appContext.Database.EnsureCreated();
            }

            //SS: ensure the connection is disposed when the tests are done
            services.AddSingleton(connection);

            builder.UseEnvironment("Development");
        });
    }

}