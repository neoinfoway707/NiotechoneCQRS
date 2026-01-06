using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs;

public class UserRoleModuleOperationModel
{
    public bool isSuccess { get; set; }
    public int statusCode { get; set; }
    public object errorMessage { get; set; }
    public List<UserRoleModuleOperation> data { get; set; }
}
