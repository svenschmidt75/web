namespace WebAPI.Model;

public class Address {
    // SS: PK
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