using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model;

public class Subject {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // SS: FK - One-To-Many
    public Category Category { get; set; }

    public string Name { get; set; }

    // SS: FK - One-To-Many
    public Teacher Teacher { get; set; }

    // SS: FK - One-To-Many
    public University University { get; set; }
}