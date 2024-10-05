namespace WebAPI.Model;

// SS: joining table for many-to-many relationsship
public class SubjectEnrolment {
    // SS: FK - Many-To-Many
    public Subject Subject { get; set; }

    // SS: FK - Many-To-Many
    public Student Student { get; set; }
}