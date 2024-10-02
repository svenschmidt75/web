namespace WebAPI.Model;

public class SubjectEnrolment {
    // SS: FK
    public int SubjectId { get; set; }

    // SS: FK
    public int StudentId { get; set; }
}