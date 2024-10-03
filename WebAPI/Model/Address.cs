using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model;

public class Address {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? UnitNumber { get; set; }

    public int StreetNumber { get; set; }

    public string StreetName { get; set; }

    public string? Suburb { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Code { get; set; }

    public string Country { get; set; }
}