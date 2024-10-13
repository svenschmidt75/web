using Microsoft.EntityFrameworkCore;
using Web.Base.Model;

namespace WebAPI;

public class TeacherDbContext : DbContext {
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<SubjectEnrolment> Enrolments { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public TeacherDbContext(DbContextOptions<TeacherDbContext> options) :
        base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<SubjectEnrolment>(
                eb => { eb.HasNoKey(); });

        // SS: seed Address table with data
        modelBuilder.Entity<Address>().HasData(
            new Address {
                Id = 1,
                City = "London",
                Code = "OX14 1DX",
                Country = "United States",
                State = "Colorado",
                Suburb = "Boulder County",
                StreetName = "Central Street",
                StreetNumber = 2366,
                UnitNumber = 12,
            });
    }
}