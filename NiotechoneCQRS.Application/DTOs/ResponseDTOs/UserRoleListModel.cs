namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs;

public class UserRoleListModel
{
    public bool isSuccess { get; set; }
    public int statusCode { get; set; }
    public object errorMessage { get; set; }
    public List<UserRoleData> data { get; set; }
}

public class UserRoleData
{
    public long UserRoleId { get; set; }
    public long CompanyId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public Nullable<int> StatusId { get; set; }
    public Nullable<int> CreatedBy { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<int> ModifiedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
}
