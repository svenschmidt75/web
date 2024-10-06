using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Repository;

public class TeacherRepository : ITeacherRepository {
    private readonly TeacherDbContext _dbContext;

    public TeacherRepository(TeacherDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<List<Address>> GetAddresses() {
        var addresses = await _dbContext.Addresses.ToListAsync();
        return addresses;
    }

    public async Task SaveChanges() {
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAddress(Address address) {
        await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();
    }
}