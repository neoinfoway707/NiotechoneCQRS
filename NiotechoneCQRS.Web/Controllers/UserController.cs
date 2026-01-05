using Microsoft.AspNetCore.Mvc;
using NiotechoneCQRS.Application.ApiRoutes;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Web.Helper;
using NiotechoneCQRS.Web.Models;

namespace NiotechoneCQRS.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly AppSettings _setting;
        public UserController(ApiClient apiClient, AppSettings settings)
        {
            _apiClient = apiClient;
            _setting = settings;
        }
        public async Task<IActionResult> Index()
        {
            string url = _setting.BaseUrl + ApiRoutes.GetAllUsers;
            var userList = await _apiClient.GetAsync<UserListModelDTO>(url);
            return View(userList);
        }

        //public async Task<IActionResult> GetUserList([DataSourceRequest] DataSourceRequest request)
        //{
        //    string url = _setting.BaseUrl + ApiRoutes.GetAllUsers;
        //    var userList = await _apiClient.GetAsync<UserListModelDTO>(url);
        //    var userView = (userList?.data ?? Enumerable.Empty<Datum>())
        //        .Select(x => new UserModel
        //        {
        //            CompanyId = x.companyId,
        //            UserId = x.userId,
        //            FullName = x.fullName,
        //            UserName = x.userName,
        //            StatusName = x.statusName?.ToString() ?? "",
        //            Email = x.email,
        //            Phone = x.phone,
        //            Address = x.address,
        //            LastLoginDate = x.lastLoginDate.ToString(),
        //            PersonalTypeId = x.personalTypeId as int?
        //        })
        //        .AsQueryable(); // Important for Kendo server-side operations

        //    // Return data with paging, sorting, filtering
        //    return Json(userView.ToDataSourceResult(request));
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.TenantName = HttpContext.Session.GetString("Company");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.CompanyId = HttpContext.Session.GetInt32("CompanyId");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            string url = _setting.BaseUrl + ApiRoutes.Create;
            var result = await _apiClient.PostAsync<UserViewModel, ResponseDTO<bool>>(url, model);

            if (result != null && result.Data)
            {
                return RedirectToAction("Index", "User");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TenantName = HttpContext.Session.GetString("Company");
            string url = _setting.BaseUrl + ApiRoutes.GetUserById + $"?id={id}";
            var response = await _apiClient.GetAsync<UserDetailDTO>(url);
            if(response != null)
            {
                UserViewModel userView = new UserViewModel
                {
                    UserId = id,
                    Address = response.data.address,
                    Email = response.data.email,
                    CompanyId = response.data.companyId,
                    FullName = response.data.fullName,
                    Phone = response.data.phone,
                    StatusId = response.data.statusId,
                    UserName = response.data.userName,
                    UserTypeId = response.data.userTypeId,
                    PasswordDecrypt = response.data.passwordDecrypt
                };
                return View(userView);
            }
            return View(new UserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            string url = _setting.BaseUrl + ApiRoutes.Update + $"?id={model.UserId}";
            var result = await _apiClient.PutAsync<UserViewModel, ResponseDTO<bool>>(url, model);

            if (result != null && result.Data)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            string url = _setting.BaseUrl + ApiRoutes.GetUserById + $"?id={id}";
            var response = await _apiClient.GetAsync<UserDetailDTO>(url);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string url = _setting.BaseUrl + ApiRoutes.DeleteUserById + $"?id={id}";
            var response = await _apiClient.DeleteAsync(url);
            return RedirectToAction("Index", "User");

        }
    }
}
