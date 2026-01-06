namespace NiotechoneCQRS.Domain.Entities;

public class CompanyLogo
{
    public long CompanyLogoId { get; set; }
    public long CompanyId { get; set; }
    public Nullable<System.DateTime> UpdatedDate { get; set; }
    public string FileName { get; set; }
    public byte[] CompanyImage { get; set; }
    public string ContentType { get; set; }
}
