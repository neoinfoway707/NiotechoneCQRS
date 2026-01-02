namespace NiotechoneCQRS.Application.Enum;

public class Enums
{
    public enum UserType
    {
        SGEAdmin = 1,
        TenentUser = 2,
        Requestor = 3,
        Admin = 4,
        Crew = 5,
        Client = 6,
        Supervisor = 7
    }

    public enum ArtifactType
    {
        CompanyHeader = 1,
        CompanyFooter = 2,
        FavImage = 3
    }

    public enum UserRole
    {
        SuperAdmin = 1
    }
}
