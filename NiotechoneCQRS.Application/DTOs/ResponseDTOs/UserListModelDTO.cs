using System.ComponentModel.DataAnnotations.Schema;
using static NiotechoneCQRS.Application.DTOs.ResponseDTOs.UserListModelDTO;

namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs
{

    public class Datum
    {
        public int userId { get; set; }
        public object userIdEncrypt { get; set; }
        public int companyId { get; set; }
        public object companyName { get; set; }
        public object firstName { get; set; }
        public object lastName { get; set; }
        public string fullName { get; set; }
        public int? userRoleId { get; set; }
        public object userRole { get; set; }
        public string address { get; set; }
        public object countryName { get; set; }
        public int? countryId { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public object newPassword { get; set; }
        public object confirmPassword { get; set; }
        public int statusId { get; set; }
        public string statusName { get; set; }
        public object createdDate { get; set; }
        public object createdBy { get; set; }
        public DateTime? modifiedDate { get; set; }
        public object lastLoginDate { get; set; }
        public int? modifyBy { get; set; }
        public object staySignedInFlag { get; set; }
        public string passwordDecrypt { get; set; }
        public object userRoleName { get; set; }
        public string token { get; set; }
        public int personalId { get; set; }
        public object isSubscription { get; set; }
        public object lastLoginCompany { get; set; }
        public int? userTypeId { get; set; }
        public object personalTypeId { get; set; }
        public int userLoginType { get; set; }
        public object loginGUID { get; set; }
    }

    public class UserListModelDTO
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public object errorMessage { get; set; }
        public List<Datum> data { get; set; }
    }

    public class UserDetailDTO
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public object errorMessage { get; set; }
        public Datum data { get; set; }
    }



    public class UserFiltersModel
    {
        public string SearchExpression { get; set; }
        public string SortExpression { get; set; }
        public string SortDirection { get; set; }
        public int? StartIndex { get; set; }
        public int? PageSize { get; set; }
    }

    public class UserColumnFilters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
        public string GeneralSearch { get; set; }
        public string StatusId { get; set; }
        public string CompanyId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsReport { get; set; }
        public string Email { get; set; }
    }

    public class UserModel
    {
        public long UserId { get; set; }
        public string UserIdEncrypt { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public long? UserRoleId { get; set; }
        public string Address { get; set; }
        public string CountryName { get; set; }
        public int? CountryId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int StatusId { get; set; }
        [NotMapped]
        public string StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string LastLoginDate { get; set; }
        public long? ModifyBy { get; set; }
        public string LastLoginCompany { get; set; }
        public int? UserTypeId { get; set; }
        public int? PersonalTypeId { get; set; }
        public byte? UserLoginType { get; set; }
        public string LoginGUID { get; set; }
    }
}
