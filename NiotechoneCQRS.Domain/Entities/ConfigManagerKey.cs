namespace NiotechoneCQRS.Domain.Entities;

public class ConfigManagerKey
{
    public int ConfigManagerKeyId { get; set; }
    public string Category { get; set; }
    public string Key { get; set; }

    public virtual ICollection<ConfigManagerValue> ConfigManagerValues { get; set; }
}
