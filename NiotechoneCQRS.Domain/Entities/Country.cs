namespace NiotechoneCQRS.Domain.Entities;

public class Country
{
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public string ShortName { get; set; }
    public string ISDCode { get; set; }
    public virtual ICollection<Company> Companies { get; set; }
}
