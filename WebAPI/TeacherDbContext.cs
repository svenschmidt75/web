using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI;

public class TeacherDbContext : DbContext {
    public DbSet<Teacher> T { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<SubjectEnrolment> Enrolments { get; set; }

    public TeacherDbContext(DbContextOptions<TeacherDbContext> options) :
        base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<SubjectEnrolment>(
                eb => { eb.HasNoKey(); });
    }
}