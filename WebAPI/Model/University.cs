namespace WebAPI.Model;

public class University {
    // SS: PK
    public int Id { get; set; }

    public string Name { get; set; }

    // SS: FK
    public int AdressId { get; set; }
}