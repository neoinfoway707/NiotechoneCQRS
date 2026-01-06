namespace NiotechoneCQRS.Application.ApiRoutes
{
    public static class ApiRoutes
    {
        public const string GetAllUsers = "get-all-users";
        public const string GetUserById = "get-user-by-id";
        public const string DeleteUserById = "delete-user-by-id";
        public const string Create = "create";
        public const string Update = "update/id";
        public const string Login = "login";

        public const string GetAllCompanies = "get-all-companies";
        public const string GetCompanyById = "get-company-by-id";
        public const string CreateCompany = "create";
        public const string UpdateCompany = "update/id";
        public const string DeleteCompany = "delete";

        public const string GetAllRoles = "get-all-roles";
        public const string GetRoleById = "get-role-by-id";
        public const string CreateRole = "create";
        public const string UpdateRole = "update/id";
        public const string DeleteRole = "delete";

        public const string GetPermissionsByRole = "get-permissions-by-role";
        public const string SavePermissionList = "save-permission-list";

        public const string GetKpiList = "get-kpi-list";
        public const string SaveKpiList = "save-kpi-list";

        public const string ConfigurationList = "configuration-list/CompanyId";
        public const string UpdateConfigurationList = "update-configuration-list/id";
        public const string SaveSystemConfigurationValue = "save-systemConfigurationValue";

        public const string GetParameterValues = "get-parameter-values";
        public const string GetConfigMasterByKey = "get-ConfigMasterByKey";
    }
}
