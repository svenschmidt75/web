using System.Linq.Expressions;
using Web.Base.Model;

namespace WebAPI.Repository;

public interface ITeacherRepository {
    Task<List<Address>> GetAddresses();
    Task<Address?> GetAddress(Expression<Func<Address, bool>>? filter);
    Task SaveChanges();
    Task AddAddress(Address address);
}