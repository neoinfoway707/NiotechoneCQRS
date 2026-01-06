namespace NiotechoneCQRS.Domain.Entities;

public class ConfigManagerValue
{
    public int ConfigManagerValueId { get; set; }
    public Nullable<int> ConfigManagerKeyId { get; set; }
    public Nullable<long> CompanyId { get; set; }
    public string Value { get; set; }
    public string Remarks { get; set; }

    public virtual ConfigManagerKey ConfigManagerKey { get; set; }
    public virtual ConfigManagerValue ConfigManagerValue1 { get; set; }
    public virtual ConfigManagerValue ConfigManagerValue2 { get; set; }
    public virtual Company Company { get; set; }
}
