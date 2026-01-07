using System.Reflection.Metadata;

namespace NiotechoneCQRS.Domain.Entities;

public class ParameterValue : BaseEntity
{
    public int ParameterValuesId { get; set; }
    public int ParameterId { get; set; }
    public string Value { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual Parameter Parameter { get; set; }
}
