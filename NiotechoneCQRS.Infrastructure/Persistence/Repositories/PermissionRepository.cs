using Dapper;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Enum;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Infrastructure.Persistence.Data;
using System.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IConfigManagerRepository _managerRepository;
    private readonly ICompanyRepository _companyRepository;
    private const string editWorkRequestTimeModule = "WO-WR-CRDTEDIT";
    private ConfigManagerValue? editWRCreatedDateConfig;

    public PermissionRepository(IDbConnectionFactory connectionFactory, IConfigManagerRepository managerRepository, ICompanyRepository companyRepository)
    {
        _managerRepository = managerRepository;
        _connectionFactory = connectionFactory;
        _companyRepository = companyRepository;
    }

    public async Task<List<UserRoleModuleOperation>> GetPermissionList(int userRoleId, CancellationToken cancellationToken = default)
    {
        var companyId = await GetCompanyByRoleId(userRoleId);

        editWRCreatedDateConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.ShowEditWRCreatedDate.ToString(), companyId);
        var editKPIModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.ShowKPIModuleConfig.ToString(), companyId);
        var editContractsModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Show_ClientContracts_ModuleConfig.ToString(), companyId);
        var editClientModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Show_Client_Module_Config.ToString(), companyId);
        var ClientContractModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Show_ClientContracts_ModuleConfig.ToString(), companyId);
        var PersonnelSLAModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Show_PersonnelSLA_ModuleConfig.ToString(), companyId);
        var SCContractsModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Show_SubContractorContracts_Config.ToString(), companyId);
        var enableSalesModuleConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Sales_Module.ToString(), companyId);
        var enableQuotationConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Enable_Quotation.ToString(), companyId);
        var enableInvoiceConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Enable_Invoicing.ToString(), companyId);
        var companyDetails = await _companyRepository.GetCompanyById(companyId);
        var isIssuanceEnabled = companyDetails!.IssuanceExist.HasValue ? companyDetails.IssuanceExist.Value : false;
        var enableViewTeamLocation = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.View_Team_Location.ToString(), companyId);
        var enableSalesRevenue = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Show_Permission_For_Sales_Revenue.ToString(), companyId);
        var enabledCapturePurchaseOrderLifeCycleDates = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Capture_Purchase_Order_Life_Cycle_Dates.ToString(), companyId);
        var enabledInvoicingWithWorkOrder = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Invoicing_With_WorkOrder.ToString(), companyId);
        var serviceReportTemplateConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Service_Report_Templates.ToString(), companyId);
        var openClosedWorkOrderConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Open_Closed_WorkOrders.ToString(), companyId);
        var additionalFields = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Additional_Fields.ToString(), companyId);
        var issuanceApprovalConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Issuance_With_Approval.ToString(), companyId);
        var rentaljobsConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Rental_Job.ToString(), companyId);
        var installationTypeConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Installation_Type.ToString(), companyId);
        var materialRequestConfig = await _managerRepository.GetConfigMasterByKey(Enums.ConfigMasterKey.Material_Request.ToString(), companyId);


        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QueryAsync<GetPermissionListResult>(
            "GetPermissionList",
            new
            {
                UserRoleId = userRoleId,
                CompanyId = companyId
            },
            commandType: CommandType.StoredProcedure
        );

        var permissionsResult = result.ToList();

        var moduleList = MapToUserRoleModuleOperations(permissionsResult, userRoleId);
        if (openClosedWorkOrderConfig != null && openClosedWorkOrderConfig.Value == "0")
        {
            var ocwModule = moduleList.SingleOrDefault(x => x.Module.ModuleCode == "WO-OCW");
            moduleList.Remove(ocwModule);
        }
        if (additionalFields != null && additionalFields.Value == "0")
        {
            var afModule = moduleList.SingleOrDefault(x => x.Module.ModuleCode == "AST-LIST-AF");
            moduleList.Remove(afModule);
            var afRModule = moduleList.SingleOrDefault(x => x.Module.ModuleCode == "AST-LIST-AFR");
            moduleList.Remove(afRModule);
        }
        // Show/Hide WorkOrder Service Report Template based on configuration - serviceReportTemplateConfig

        if (serviceReportTemplateConfig != null && serviceReportTemplateConfig.Value == "0")
        {
            var srtModule = moduleList.SingleOrDefault(x => x.Module.ModuleCode == "WO-SRT");
            moduleList.Remove(srtModule);
        }

        if (isIssuanceEnabled == false && issuanceApprovalConfig.Value == "0")
        {
            var issueRptModule = moduleList.SingleOrDefault(x => x.Module.ModuleCode == "RPT-INV-ISSURTRN");
            moduleList.Remove(issueRptModule);
        }


        moduleList.ForEach(x =>
        {
            if (x.Module.ModuleCode == "PER-IN-SLA" && (PersonnelSLAModuleConfig == null || PersonnelSLAModuleConfig.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "RPT-CON" && (editContractsModuleConfig == null || editContractsModuleConfig.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "WO-QUOT" && (enableQuotationConfig == null || enableQuotationConfig.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "WO-INV" && (enableInvoiceConfig == null || enableInvoiceConfig.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "INV-ISSLIST" && !isIssuanceEnabled)
            {
                x.Module.ParentModule = null;
            }
            if (x.Module.ModuleCode == "INV-STKTRANSLIST" && !isIssuanceEnabled)
            {
                x.Module.ParentModule = null;
            }
            if ((x.Module.ModuleCode == "SALES-ENQ" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0")) || (x.Module.ModuleCode == "SALES-QUO" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0")) || (x.Module.ModuleCode == "SALES-INV" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0")))
            {
                x.Module.ParentModule = null;
            }
            if ((x.Module.ModuleCode == "RPT-SALES" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0")) || (x.Module.ModuleCode == "PER-STAFF" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0")) || (x.Module.ModuleCode == "PROS" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0")))
            {
                x.Module.ParentModule = null;
            }
            if (x.Module.ModuleCode == "TEAM-LOCATION" && (enableViewTeamLocation == null || enableViewTeamLocation.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "CLI-CON-SALREVE" && (enableSalesRevenue == null || enableSalesRevenue.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "CLI-CON" && (ClientContractModuleConfig == null || ClientContractModuleConfig.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "PER-SC-CON" && (SCContractsModuleConfig == null || SCContractsModuleConfig.Value == "0"))
            {
                x.Module.ParentModule = null;
            }

            if (x.Module.ModuleCode == "INV-POLIST-POD" && (enabledCapturePurchaseOrderLifeCycleDates == null || enabledCapturePurchaseOrderLifeCycleDates.Value == "0"))
            {
                x.Module.ParentModule = null;
            }
            if (x.Module.ModuleCode == "SALES-WO-INV" && (enabledInvoicingWithWorkOrder == null || enabledInvoicingWithWorkOrder.Value == "0"))
            {
                x.Module.ParentModule = null;
            }
            if ((x.Module.ModuleCode == "RENTJOB-JOBS" && (rentaljobsConfig == null || rentaljobsConfig.Value == "0")) || (x.Module.ModuleCode == "RENTJOB-RTOOL" && (rentaljobsConfig == null || rentaljobsConfig.Value == "0")))
            {
                x.Module.ParentModule = null;
            }
            if ((x.Module.ModuleCode == "AST-SYNC" && (installationTypeConfig == null || installationTypeConfig.Value == "0")))
            {
                x.Module.ParentModule = null;
            }
            if ((x.Module.ModuleCode == "RPT-RENT" && (rentaljobsConfig == null || rentaljobsConfig.Value == "0")))
            {
                x.Module.ParentModule = null;
            }
            if ((x.Module.ModuleCode == "MATREQLIST" && (materialRequestConfig == null || materialRequestConfig.Value == "0")))
            {
                x.Module.ParentModule = null;
            }
        });

        var parentModules = MapToUserRoleModuleOperationsForParentModule(permissionsResult, userRoleId);
        List<UserRoleModuleOperation> RoleModuleOperationList = new List<UserRoleModuleOperation>();

        foreach (var module in parentModules)
        {
            if (module.Module.ModuleCode == "KPI" && (editKPIModuleConfig == null || editKPIModuleConfig.Value == "0"))
            {
                continue;
            }

            if (module.Module.ModuleCode == "CLI" && (editClientModuleConfig == null || editClientModuleConfig.Value == "0"))
            {
                continue;
            }
            if (module.Module.ModuleCode == "SALES" && (enableSalesModuleConfig == null || enableSalesModuleConfig.Value == "0"))
            {
                continue;
            }
            if (module.Module.ModuleCode == "RENTJOB" && (rentaljobsConfig == null || rentaljobsConfig.Value == "0"))
            {
                continue;
            }
            AddToPermissinList(ref RoleModuleOperationList, module, moduleList);
        }

        return RoleModuleOperationList;
    }

    public async Task<string?> SavePermissionList(SaveUserRolePermissionList saveUserRolePermissionList, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<string>(
            "SaveRolePermission",
            new
            {
                UserRoleId = saveUserRolePermissionList.UserRoleId,
                RolePermission_XML = saveUserRolePermissionList.RoleListXml
            },
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public async Task<List<KPIList>> GetKpiList(long companyId, long userRoleId, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QueryAsync<DashboardKPIList>(
            "GetDashboardKPIListByUserRole",
            new
            {
                CompanyId = companyId,
                UserRoleId = userRoleId,
            },
            commandType: CommandType.StoredProcedure
        );

        var kpiList = new List<KPIList>();

        foreach (var item in result)
        {
            string defaultRange = item.DefaultDateRange switch
            {
                "This Month" => "This Month",
                "Till Date" => "Till Date",
                _ => string.Empty
            };

            var mappedResult = new DashboardKPIList
            {
                Control = item.Control,
                KPIMasterId = item.KPIMasterId,
                Name = item.Name,
                CompanyId = item.CompanyId ?? companyId,
                UserRoleId = item.UserRoleId ?? userRoleId,
                ChartType = item.ChartType,
                KPIDataType = item.KPIDataType,
                DefaultDateRange = defaultRange,
                OrderInDashboard = item.OrderInDashboard ?? 0,
                RedirectionToGrid = item.RedirectionToGrid
            };

            kpiList.Add(new KPIList
            {
                getDashboardKPIList = mappedResult,
                Visibility = item.UserRoleId != null ? true : false
            });
        }

        return kpiList;
    }

    public async Task<bool> SaveKpiList(List<KPIList> kpiList, CancellationToken cancellation = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            foreach (var model in kpiList)
            {
                var existingKpiId = await connection.QueryFirstOrDefaultAsync<long?>(
                    @"SELECT KPIAllocationId
                  FROM KPIAllocation
                  WHERE CompanyId = @CompanyId
                    AND UserRoleId = @UserRoleId
                    AND KPIMasterId = @KPIMasterId",
                    new
                    {
                        model.getDashboardKPIList.CompanyId,
                        model.getDashboardKPIList.UserRoleId,
                        model.getDashboardKPIList.KPIMasterId
                    },
                    transaction
                );

                // ❌ Visibility false → DELETE
                if (existingKpiId.HasValue && model.Visibility == false)
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM KPIAllocation
                      WHERE KPIAllocationId = @Id",
                        new { Id = existingKpiId.Value },
                        transaction
                    );
                }
                // ✅ Visibility true
                else if (model.Visibility == true)
                {
                    // ➕ INSERT
                    if (!existingKpiId.HasValue)
                    {
                        await connection.ExecuteAsync(
                            @"INSERT INTO KPIAllocation
                          (CompanyId, UserRoleId, KPIMasterId, OrderInDashboard)
                          VALUES
                          (@CompanyId, @UserRoleId, @KPIMasterId, @OrderInDashboard)",
                            new
                            {
                                model.getDashboardKPIList.CompanyId,
                                model.getDashboardKPIList.UserRoleId,
                                model.getDashboardKPIList.KPIMasterId,
                                model.getDashboardKPIList.OrderInDashboard
                            },
                            transaction
                        );
                    }
                    // ✏ UPDATE
                    else
                    {
                        await connection.ExecuteAsync(
                            @"UPDATE KPIAllocation
                          SET OrderInDashboard = @OrderInDashboard
                          WHERE KPIAllocationId = @Id",
                            new
                            {
                                Id = existingKpiId.Value,
                                model.getDashboardKPIList.OrderInDashboard
                            },
                            transaction
                        );
                    }
                }
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task<long> GetCompanyByRoleId(int roleId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT CompanyId
            FROM UserRole
            WHERE UserRoleId = @UserRoleId"
            ;

        var companyId = await connection.QueryFirstOrDefaultAsync<long?>(
            sql,
            new { UserRoleId = roleId }
        );

        return companyId ?? 0;
    }

    private static List<UserRoleModuleOperation> MapToUserRoleModuleOperations(List<GetPermissionListResult> permissionsResult, long userRoleId)
    {
        var result = new List<UserRoleModuleOperation>();

        foreach (var item in permissionsResult)
        {
            if (string.IsNullOrWhiteSpace(item.OperationId))
                continue;

            var operationIds = item.OperationId
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            foreach (var operationId in operationIds)
            {
                result.Add(new UserRoleModuleOperation
                {
                    UserRoleId = userRoleId,
                    ModuleId = item.ModuleId,
                    OperationId = operationId,
                    StatusId = 1,

                    Module = new Module
                    {
                        ModuleId = item.ModuleId,
                        ModuleName = item.ModuleName,
                        ModuleCode = item.ModuleCode,
                        ParentModule = item.ParentModule,
                        ModuleLevel = item.ModuleLevel,
                        SubLevelOrder = item.SubLevelOrder,
                        Seq = item.Seq
                    }
                });
            }
        }

        return result
            .OrderBy(x => x.Module?.Seq)
            .ThenBy(x => x.OperationId)
            .ToList();
    }

    private static List<UserRoleModuleOperation> MapToUserRoleModuleOperationsForParentModule(IEnumerable<GetPermissionListResult> source, long userRoleId)
    {
        var result = new List<UserRoleModuleOperation>();

        foreach (var item in source)
        {
            // Same filter: Seq != null && Seq > 0
            if (!item.Seq.HasValue || item.Seq <= 0)
                continue;

            // OperationId is CSV → split into multiple operations
            if (string.IsNullOrWhiteSpace(item.OperationId))
                continue;

            var operationIds = item.OperationId
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            foreach (var operationId in operationIds)
            {
                result.Add(new UserRoleModuleOperation
                {
                    UserRoleId = userRoleId,
                    ModuleId = item.ModuleId,
                    OperationId = operationId,
                    StatusId = 1,

                    Module = new Module
                    {
                        ModuleId = item.ModuleId,
                        ModuleName = item.ModuleName,
                        ModuleCode = item.ModuleCode,
                        ParentModule = item.ParentModule,
                        ModuleLevel = item.ModuleLevel,
                        SubLevelOrder = item.SubLevelOrder,
                        Seq = item.Seq
                    }
                });
            }
        }

        return result
            .OrderBy(x => x.Module.Seq)
            .ToList();
    }

    private void AddToPermissinList(ref List<UserRoleModuleOperation> roleModuleOperationList, UserRoleModuleOperation module, List<UserRoleModuleOperation> moduleList)
    {
        if (module.Module.ModuleCode == editWorkRequestTimeModule && editWRCreatedDateConfig?.Value == "0")
        {
            return;
        }
        roleModuleOperationList.Add(new UserRoleModuleOperation
        {
            UserRoleModuleOperationId = module.UserRoleModuleOperationId,
            UserRoleId = module.UserRoleId,
            ModuleId = module.ModuleId,
            OperationId = module.OperationId,
            StatusId = module.StatusId,

            Module = new Module
            {
                ModuleId = module.ModuleId,
                ModuleName = module.Module.ModuleName,
                ModuleCode = module.Module.ModuleCode,
                ParentModule = module.Module.ParentModule,
                ModuleLevel = module.Module.ModuleLevel,
                SubLevelOrder = module.Module.SubLevelOrder,
                Seq = module.Module.Seq
            }
        });

        var childModules = moduleList
            .Where(a => a.Module.ParentModule == module.ModuleId)
            .OrderBy(a => a.Module.SubLevelOrder)
            .ToList();


        if (childModules.Any())
        {
            foreach (var child in childModules)
            {
                AddToPermissinList(ref roleModuleOperationList, child, moduleList);
            }
        }
    }
}
