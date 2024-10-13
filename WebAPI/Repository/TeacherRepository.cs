using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Web.Base.Model;

namespace WebAPI.Repository;

public class TeacherRepository(TeacherDbContext dbContext) : ITeacherRepository {

    public async Task<List<Address>> GetAddresses() {
        var addresses = await dbContext.Addresses.ToListAsync();
        return addresses;
    }

    public async Task<Address?> GetAddress(Expression<Func<Address, bool>>? filter) {
        IQueryable<Address> query = dbContext.Addresses;

        if (filter != null) {
            query = query.Where(filter);
        }

        var addresses = await query.ToListAsync();
        return addresses.FirstOrDefault();
    }

    public async Task SaveChanges() {
        await dbContext.SaveChangesAsync();
    }

    public async Task AddAddress(Address address) {
        await dbContext.Addresses.AddAsync(address);
        await dbContext.SaveChangesAsync();
    }
}