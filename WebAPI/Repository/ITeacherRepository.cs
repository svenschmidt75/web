using WebAPI.Model;

namespace WebAPI.Repository;

public interface ITeacherRepository {
    Task<List<Address>> GetAddresses();
    Task SaveChanges();
    Task AddAddress(Address address);
}