namespace NiotechoneCQRS.Domain.Entities;

public class ParameterValue
{
    public int ParameterValuesId { get; set; }
    public int ParameterId { get; set; }
    public string Value { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Nullable<int> CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<int> ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }

    public virtual Parameter Parameter { get; set; }
}

