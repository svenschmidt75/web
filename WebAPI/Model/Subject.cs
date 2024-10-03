using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model;

public class Subject {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    // SS: FK
    public int CategoryId { get; set; }

    // SS: FK
    public int TeacherId { get; set; }

    // SS: FK
    public int UniversityId { get; set; }
}