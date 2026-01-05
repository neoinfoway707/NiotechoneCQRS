namespace NiotechoneCQRS.Domain.Entities;

public class CompanyArtifact
{
    public long ArtifactId { get; set; }
    public long CompanyId { get; set; }
    public string Filename { get; set; }
    public string ContentType { get; set; }
    public Nullable<byte> ArtifactType { get; set; }
    public Nullable<System.DateTime> UpdatedDate { get; set; }
    public long UpdatedBy { get; set; }
    public byte[] CompanyImage { get; set; }
    public virtual Company? Company { get; set; }
}
