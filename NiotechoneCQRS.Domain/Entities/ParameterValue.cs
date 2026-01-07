using System.Reflection.Metadata;

namespace NiotechoneCQRS.Domain.Entities;

public class ParameterValue : BaseEntity
{
    public int ParameterValuesId { get; set; }
    public int ParameterId { get; set; }
    public string Value { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }   
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
