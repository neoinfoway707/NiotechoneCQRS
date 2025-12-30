using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs
{
    public class UserListModelDTO
    {
        public IList<UserModel> UserList { get; set; }
        public UserFiltersModel ParamsCompany { get; set; }
        public int TotalRecord { get; set; }
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
