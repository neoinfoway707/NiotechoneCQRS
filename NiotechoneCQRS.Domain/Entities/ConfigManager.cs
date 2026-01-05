namespace NiotechoneCQRS.Domain.Entities;

public class ConfigManager
{
    /// <summary>
    /// Gets or sets the configuration manager identifier.
    /// </summary>
    /// <value>
    /// The configuration manager identifier.
    /// </value>
    public int ConfigManagerId { get; set; }

    /// <summary>
    /// Gets or sets the configuration manager value identifier.
    /// </summary>
    /// <value>
    /// The configuration manager value identifier.
    /// </value>
    public int ConfigManagerValueId { get; set; }

    /// <summary>
    /// Gets or sets the company identifier.
    /// </summary>
    /// <value>
    /// The company identifier.
    /// </value>
    public long? CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    /// <value>
    /// The category.
    /// </value>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>
    /// The key.
    /// </value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the remarks.
    /// </summary>
    /// <value>
    /// The remarks.
    /// </value>
    public string Remarks { get; set; }
}
