namespace Web.Base.Model;

public class AddressDTO {
    public int Id { get; set; }

    public int? UnitNumber { get; set; }

    public int StreetNumber { get; set; }

    public string StreetName { get; set; }

    public string? Suburb { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Code { get; set; }

    public string Country { get; set; }

    protected bool Equals(AddressDTO other) {
        return Id == other.Id && UnitNumber == other.UnitNumber && StreetNumber == other.StreetNumber &&
               StreetName == other.StreetName && Suburb == other.Suburb && City == other.City && State == other.State &&
               Code == other.Code && Country == other.Country;
    }

    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AddressDTO)obj);
    }

    public override int GetHashCode() {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(UnitNumber);
        hashCode.Add(StreetNumber);
        hashCode.Add(StreetName);
        hashCode.Add(Suburb);
        hashCode.Add(City);
        hashCode.Add(State);
        hashCode.Add(Code);
        hashCode.Add(Country);
        return hashCode.ToHashCode();
    }
}