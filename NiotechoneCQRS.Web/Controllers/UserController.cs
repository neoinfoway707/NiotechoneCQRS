using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiClient _apiClient;
        public UserController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> Index()
        {
            string url = "https://localhost:7121/get-all-users";

            var response = await _apiClient.GetAsync<ResponseDTO<UserListModelDTO>>(
               url);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUserList(DataSourceRequest request, long? companyId)
        {
                        UserColumnFilters columnFilters = new UserColumnFilters();
            var userList = await _apiClient.GetAsync<UserListModelDTO>(
               "/api/users");
            var userView = (userList?.UserList ?? Enumerable.Empty<UserModel>()).Select(x => new UserModel
            {
                CompanyId = x.CompanyId,
                UserId = x.UserId,
                FullName = x.FullName,
                UserName = x.UserName,
                StatusName = x.StatusName,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address,
                LastLoginDate = x.LastLoginDate,
                PersonalTypeId = x.PersonalTypeId
            }).AsQueryable();

            DataSourceResult result = await userView.ToDataSourceResultAsync(request);

            return Json(result);
        }

    }
}
