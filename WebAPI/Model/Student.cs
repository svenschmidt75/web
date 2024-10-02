namespace WebAPI.Model;

public class Student {
    // SS: PK
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly Birthday { get; set; }

    // SS: FK
    public int AddressId { get; set; }
}
