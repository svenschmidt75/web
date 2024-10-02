namespace WebAPI.Model;

public class Subject {
    // SS: PK
    public int Id { get; set; }

    public string Name { get; set; }

    // SS: FK
    public int CategoryId { get; set; }

    // SS: FK
    public int TeacherId { get; set; }

    // SS: FK
    public int UniversityId { get; set; }
}