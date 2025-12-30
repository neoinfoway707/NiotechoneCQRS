namespace NiotechoneCQRS.Domain.Entities;

public class ParameterValue
{
    /// <summary>
    /// Gets or sets the parameter values identifier.
    /// </summary>
    /// <value>
    /// The parameter values identifier.
    /// </value>
    public int ParameterValuesId { get; set; }

    /// <summary>
    /// Gets or sets the parameter identifier.
    /// </summary>
    /// <value>
    /// The parameter identifier.
    /// </value>
    public int? ParameterId { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    /// <value>
    /// The created by.
    /// </value>
    public long? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the modified by.
    /// </summary>
    /// <value>
    /// The modified by.
    /// </value>
    public long? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the modified date.
    /// </summary>
    /// <value>
    /// The modified date.
    /// </value>
    public DateTime? ModifiedDate { get; set; }

    public string ParameterName { get; set; }
}

public class WorkStatusColorsModel
{
    public int StatusId { get; set; }
    public string Status { get; set; }
    public string ColorAssigned { get; set; }
}
