using Microsoft.EntityFrameworkCore;

namespace WebAPI.Test;

public class TestDBContext : DbContext {
    public TestDBContext(DbContextOptions options) : base(options) { }

}