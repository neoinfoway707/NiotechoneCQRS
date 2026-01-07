namespace NiotechoneCQRS.Domain.Entities;

public class Parameter
{
    public int ParameterId { get; set; }
    public string ParamName { get; set; }

    public virtual ICollection<ParameterValue> ParameterValues { get; set; }
}
