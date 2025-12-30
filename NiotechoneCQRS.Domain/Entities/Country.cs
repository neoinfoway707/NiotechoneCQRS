namespace NiotechoneCQRS.Domain.Entities;

public class Country
{
    /// <summary>
    /// Gets or sets the country identifier.
    /// </summary>
    /// <value>
    /// The country identifier.
    /// </value>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    /// <value>
    /// The name of the country.
    /// </value>
    public string CountryName { get; set; }

    /// <summary>
    /// Gets or sets the short name.
    /// </summary>
    /// <value>
    /// The short name.
    /// </value>
    public string ShortName { get; set; }

    /// <summary>
    /// Gets or sets the isd code.
    /// </summary>
    /// <value>
    /// The isd code.
    /// </value>
    public string ISDCode { get; set; }
}
